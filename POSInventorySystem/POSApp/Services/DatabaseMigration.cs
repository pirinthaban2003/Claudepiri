using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using POSApp.Data;

namespace POSApp.Services
{
    public class DatabaseMigration
    {
        private readonly DatabaseHelper _dbHelper;

        public DatabaseMigration()
        {
            _dbHelper = new DatabaseHelper();
        }

        public void EnsureSchemaUpToDate()
        {
            try
            {
                // Try to create DB if it doesn't exist
                try { EnsureDatabaseExists(); } catch { /* Might already exist or no permission to master */ }

                using (var conn = _dbHelper.GetConnection())
                {
                    conn.Open();

                    // 1. Ensure core tables exist (Basic subset to avoid ALTER failures)
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Categories (CategoryID INT AUTO_INCREMENT PRIMARY KEY, CategoryName VARCHAR(100) NOT NULL UNIQUE)");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Suppliers (SupplierID INT AUTO_INCREMENT PRIMARY KEY, SupplierName VARCHAR(255) NOT NULL)");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Products (ProductID INT AUTO_INCREMENT PRIMARY KEY, ProductName VARCHAR(255) NOT NULL, Price DECIMAL(10, 2) NOT NULL, StockQuantity INT DEFAULT 0)");

                    // 2. Add missing columns to Products
                    string[] columnsToAdd = {
                        "SKU VARCHAR(50)",
                        "Barcode VARCHAR(50)",
                        "CategoryID INT",
                        "SupplierID INT",
                        "BranchID INT",
                        "TaxCategoryID INT",
                        "Brand VARCHAR(100)",
                        "UnitType VARCHAR(20)",
                        "MinStockLevel INT DEFAULT 10",
                        "DiscountRate DECIMAL(5, 2) DEFAULT 0.00",
                        "IsBOGO BOOLEAN DEFAULT FALSE",
                        "IsActive BOOLEAN DEFAULT TRUE"
                    };

                    foreach (var colDef in columnsToAdd)
                    {
                        string colName = colDef.Split(' ')[0];
                        if (!ColumnExists(conn, "Products", colName))
                        {
                            ExecuteNonQuery(conn, $"ALTER TABLE Products ADD COLUMN {colDef}");
                        }
                    }

                    // 3. Ensure TaxCategories table and data
                    ExecuteNonQuery(conn, @"
                        CREATE TABLE IF NOT EXISTS TaxCategories (
                            TaxCategoryID INT AUTO_INCREMENT PRIMARY KEY,
                            TaxName VARCHAR(50) NOT NULL,
                            TaxPercentage DECIMAL(5, 2) NOT NULL
                        )");

                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM TaxCategories", conn))
                    {
                        if (Convert.ToInt64(cmd.ExecuteScalar()) == 0)
                        {
                            ExecuteNonQuery(conn, "INSERT INTO TaxCategories (TaxName, TaxPercentage) VALUES ('Standard VAT', 15.00), ('Luxury Tax', 18.00), ('Zero Rated', 0.00)");
                        }
                    }

                    // 4. Ensure other vital tables for POS/Sales/Inventory
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Sales (SaleID INT AUTO_INCREMENT PRIMARY KEY, SaleDate DATETIME DEFAULT CURRENT_TIMESTAMP, TotalAmount DECIMAL(10,2), FinalAmount DECIMAL(10,2), CustomerID INT, UserID INT, BranchID INT, DiscountAmount DECIMAL(10,2), TaxAmount DECIMAL(10,2))");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS SaleItems (SaleItemID INT AUTO_INCREMENT PRIMARY KEY, SaleID INT, ProductID INT, Quantity INT, Subtotal DECIMAL(10,2), UnitPrice DECIMAL(10,2), Discount DECIMAL(10,2))");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS InventoryBatches (BatchID INT AUTO_INCREMENT PRIMARY KEY, ProductID INT, Quantity INT, InitialQuantity INT, CostPrice DECIMAL(10,2), SellingPrice DECIMAL(10,2), BatchNumber VARCHAR(50), ExpiryDate DATE, ReceivedDate DATETIME DEFAULT CURRENT_TIMESTAMP)");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Customers (CustomerID INT AUTO_INCREMENT PRIMARY KEY, CustomerName VARCHAR(100) NOT NULL, Phone VARCHAR(20) UNIQUE, LoyaltyPoints INT DEFAULT 0, LoyaltyLevel VARCHAR(50) DEFAULT 'Bronze')");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS AuditLogs (LogID INT AUTO_INCREMENT PRIMARY KEY, UserID INT, Action VARCHAR(255), ModuleName VARCHAR(100), Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP)");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Roles (RoleID INT AUTO_INCREMENT PRIMARY KEY, RoleName VARCHAR(50) NOT NULL UNIQUE)");
                    ExecuteNonQuery(conn, "CREATE TABLE IF NOT EXISTS Users (UserID INT AUTO_INCREMENT PRIMARY KEY, Username VARCHAR(50) NOT NULL UNIQUE, PasswordHash VARCHAR(255) NOT NULL, RoleID INT, FullName VARCHAR(100), IsActive BOOLEAN DEFAULT TRUE)");

                    // Seed Roles if empty
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM Roles", conn))
                    {
                        if (Convert.ToInt64(cmd.ExecuteScalar()) == 0)
                        {
                            ExecuteNonQuery(conn, "INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Manager'), ('Cashier'), ('Inventory Staff')");
                        }
                    }

                    // Seed Admin if empty
                    using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM Users", conn))
                    {
                        if (Convert.ToInt64(cmd.ExecuteScalar()) == 0)
                        {
                            ExecuteNonQuery(conn, "INSERT INTO Users (Username, PasswordHash, RoleID, FullName) VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1, 'System Administrator')");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try { System.IO.File.WriteAllText("migration_error.log", DateTime.Now + ": " + ex.ToString()); } catch { }
            }
        }

        private void EnsureDatabaseExists()
        {
            string masterConnString = $"Server={Configuration.Server};Uid={Configuration.User};Pwd={Configuration.Password};";
            using (var conn = new MySqlConnection(masterConnString))
            {
                conn.Open();
                using (var cmd = new MySqlCommand($"CREATE DATABASE IF NOT EXISTS {Configuration.Database}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool ColumnExists(MySqlConnection conn, string tableName, string columnName)
        {
            string query = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table AND COLUMN_NAME = @column";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@schema", conn.Database);
                cmd.Parameters.AddWithValue("@table", tableName);
                cmd.Parameters.AddWithValue("@column", columnName);
                var result = cmd.ExecuteScalar();
                return result != null && Convert.ToInt32(result) > 0;
            }
        }

        private void ExecuteNonQuery(MySqlConnection conn, string query)
        {
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
