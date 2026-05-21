using MySql.Data.MySqlClient;
using System.Data;
using POSApp.Models;
using System.Collections.Generic;

namespace POSApp.Data
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper()
        {
            connectionString = Configuration.ConnectionString;
        }

        public DatabaseHelper(string server, string database, string uid, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query, MySqlParameter[]? parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query, MySqlParameter[]? parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object? ExecuteScalar(string query, MySqlParameter[]? parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Supplier specific methods
        public DataTable GetAllSuppliers()
        {
            return ExecuteQuery("SELECT * FROM Suppliers");
        }

        public int AddSupplier(Supplier supplier)
        {
            string query = "INSERT INTO Suppliers (SupplierName, ContactPerson, Phone, Email, Address) VALUES (@name, @contact, @phone, @email, @address)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@name", supplier.SupplierName),
                new MySqlParameter("@contact", (object?)supplier.ContactPerson ?? DBNull.Value),
                new MySqlParameter("@phone", (object?)supplier.Phone ?? DBNull.Value),
                new MySqlParameter("@email", (object?)supplier.Email ?? DBNull.Value),
                new MySqlParameter("@address", (object?)supplier.Address ?? DBNull.Value)
            };
            return ExecuteNonQuery(query, parameters);
        }

        public int UpdateSupplier(Supplier supplier)
        {
            string query = "UPDATE Suppliers SET SupplierName=@name, ContactPerson=@contact, Phone=@phone, Email=@email, Address=@address WHERE SupplierID=@id";
            MySqlParameter[] parameters = {
                new MySqlParameter("@name", supplier.SupplierName),
                new MySqlParameter("@contact", (object?)supplier.ContactPerson ?? DBNull.Value),
                new MySqlParameter("@phone", (object?)supplier.Phone ?? DBNull.Value),
                new MySqlParameter("@email", (object?)supplier.Email ?? DBNull.Value),
                new MySqlParameter("@address", (object?)supplier.Address ?? DBNull.Value),
                new MySqlParameter("@id", supplier.SupplierID)
            };
            return ExecuteNonQuery(query, parameters);
        }

        public int DeleteSupplier(int supplierId)
        {
            return ExecuteNonQuery("DELETE FROM Suppliers WHERE SupplierID=@id", new MySqlParameter[] { new MySqlParameter("@id", supplierId) });
        }
    }
}
