using System;
using System.Collections.Generic;

namespace POSApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleID { get; set; }
        public int? BranchID { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? RoleName { get; set; }
        public string? BranchName { get; set; }
    }

    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }

    public class Branch
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string? Location { get; set; }
        public bool IsHeadOffice { get; set; }
    }

    public class TaxCategory
    {
        public int TaxCategoryID { get; set; }
        public string TaxName { get; set; } = string.Empty;
        public decimal TaxPercentage { get; set; }
    }

    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; }
    }

    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string? SKU { get; set; }
        public string? Barcode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int? CategoryID { get; set; }
        public int? SupplierID { get; set; }
        public int? BranchID { get; set; }
        public int? TaxCategoryID { get; set; }
        public string? Brand { get; set; }
        public string? UnitType { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public bool IsActive { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public string? BranchName { get; set; }
    }

    public class InventoryBatch
    {
        public int BatchID { get; set; }
        public int ProductID { get; set; }
        public string? BatchNumber { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public int InitialQuantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ReceivedDate { get; set; }
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int LoyaltyPoints { get; set; }
        public decimal WalletBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Sale
    {
        public int SaleID { get; set; }
        public int? CustomerID { get; set; }
        public int? UserID { get; set; }
        public int? BranchID { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
    }

    public class SaleItem
    {
        public int SaleItemID { get; set; }
        public int SaleID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class AuditLog
    {
        public int LogID { get; set; }
        public int? UserID { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? ModuleName { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IPAddress { get; set; }
        public string? Username { get; set; }
    }

    public class Expense
    {
        public int ExpenseID { get; set; }
        public string ExpenseTitle { get; set; } = string.Empty;
        public string? Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? Description { get; set; }
    }
}
