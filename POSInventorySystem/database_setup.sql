CREATE DATABASE IF NOT EXISTS pos_db;
USE pos_db;

-- Enterprise Scalability: Multi-Branch Support
CREATE TABLE IF NOT EXISTS Branches (
    BranchID INT AUTO_INCREMENT PRIMARY KEY,
    BranchName VARCHAR(100) NOT NULL,
    Location VARCHAR(255),
    IsHeadOffice BOOLEAN DEFAULT FALSE
);

-- Enterprise Scalability: Detailed Tax Management
CREATE TABLE IF NOT EXISTS TaxCategories (
    TaxCategoryID INT AUTO_INCREMENT PRIMARY KEY,
    TaxName VARCHAR(50) NOT NULL,
    TaxPercentage DECIMAL(5, 2) NOT NULL
);

-- Authentication & Security Module
CREATE TABLE IF NOT EXISTS Roles (
    RoleID INT AUTO_INCREMENT PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleID INT,
    BranchID INT,
    FullName VARCHAR(100),
    Email VARCHAR(100),
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID) ON DELETE RESTRICT,
    FOREIGN KEY (BranchID) REFERENCES Branches(BranchID) ON DELETE SET NULL
);

-- Supplier Management Module
CREATE TABLE IF NOT EXISTS Suppliers (
    SupplierID INT AUTO_INCREMENT PRIMARY KEY,
    SupplierName VARCHAR(255) NOT NULL,
    ContactPerson VARCHAR(100),
    Phone VARCHAR(20),
    Email VARCHAR(100),
    Address TEXT,
    Balance DECIMAL(18, 2) DEFAULT 0.00
);

-- Product Management Module
CREATE TABLE IF NOT EXISTS Categories (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Products (
    ProductID INT AUTO_INCREMENT PRIMARY KEY,
    SKU VARCHAR(50) UNIQUE,
    Barcode VARCHAR(50) UNIQUE,
    ProductName VARCHAR(255) NOT NULL,
    CategoryID INT,
    SupplierID INT,
    BranchID INT,
    TaxCategoryID INT,
    Brand VARCHAR(100),
    UnitType VARCHAR(20), -- e.g., kg, pcs, ltr
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    MinStockLevel INT DEFAULT 10,
    DiscountRate DECIMAL(5, 2) DEFAULT 0.00,
    IsBOGO BOOLEAN DEFAULT FALSE,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) ON DELETE SET NULL,
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID) ON DELETE SET NULL,
    FOREIGN KEY (BranchID) REFERENCES Branches(BranchID) ON DELETE SET NULL,
    FOREIGN KEY (TaxCategoryID) REFERENCES TaxCategories(TaxCategoryID) ON DELETE SET NULL
);

-- Inventory Management Module (Batch Management)
CREATE TABLE IF NOT EXISTS InventoryBatches (
    BatchID INT AUTO_INCREMENT PRIMARY KEY,
    ProductID INT,
    BatchNumber VARCHAR(50),
    CostPrice DECIMAL(10, 2) NOT NULL,
    SellingPrice DECIMAL(10, 2) NOT NULL,
    Quantity INT NOT NULL,
    InitialQuantity INT NOT NULL,
    ExpiryDate DATE,
    ReceivedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS StockHistory (
    HistoryID INT AUTO_INCREMENT PRIMARY KEY,
    ProductID INT,
    ChangeType ENUM('SALE', 'PURCHASE', 'ADJUSTMENT', 'RETURN', 'DAMAGE') NOT NULL,
    QuantityChanged INT NOT NULL,
    ReferenceID INT, -- SaleID or PurchaseOrderID
    Remarks TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Customer Management Module
CREATE TABLE IF NOT EXISTS Customers (
    CustomerID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerName VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) UNIQUE,
    Email VARCHAR(100),
    LoyaltyPoints INT DEFAULT 0,
    LoyaltyLevel VARCHAR(50) DEFAULT 'Bronze',
    WalletBalance DECIMAL(10, 2) DEFAULT 0.00,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- POS Billing Module
CREATE TABLE IF NOT EXISTS Sales (
    SaleID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,
    UserID INT, -- Cashier
    BranchID INT,
    SaleDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    DiscountAmount DECIMAL(10, 2) DEFAULT 0.00,
    TaxAmount DECIMAL(10, 2) DEFAULT 0.00,
    FinalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE SET NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (BranchID) REFERENCES Branches(BranchID) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS SaleItems (
    SaleItemID INT AUTO_INCREMENT PRIMARY KEY,
    SaleID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2) DEFAULT 0.00,
    Subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE RESTRICT
);

-- Returns & Refunds
CREATE TABLE IF NOT EXISTS Returns (
    ReturnID INT AUTO_INCREMENT PRIMARY KEY,
    SaleID INT,
    UserID INT,
    ReturnDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalRefundAmount DECIMAL(10, 2) NOT NULL,
    Reason TEXT,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID) ON DELETE CASCADE,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS ReturnItems (
    ReturnItemID INT AUTO_INCREMENT PRIMARY KEY,
    ReturnID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    RefundAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ReturnID) REFERENCES Returns(ReturnID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE RESTRICT
);

-- Expenses Module
CREATE TABLE IF NOT EXISTS Expenses (
    ExpenseID INT AUTO_INCREMENT PRIMARY KEY,
    ExpenseTitle VARCHAR(255) NOT NULL,
    Category VARCHAR(100), -- Rent, Utilities, Salary, etc.
    Amount DECIMAL(10, 2) NOT NULL,
    ExpenseDate DATE,
    Description TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Payments (
    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
    SaleID INT,
    PaymentMethod ENUM('CASH', 'CARD', 'QR', 'WALLET') NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    TransactionRef VARCHAR(100),
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID) ON DELETE CASCADE
);

-- Promotions & Offers
CREATE TABLE IF NOT EXISTS Offers (
    OfferID INT AUTO_INCREMENT PRIMARY KEY,
    OfferName VARCHAR(100) NOT NULL,
    OfferType ENUM('PERCENTAGE', 'FIXED', 'BOGO', 'COMBO') NOT NULL,
    Value DECIMAL(10, 2),
    StartDate DATE,
    EndDate DATE,
    IsActive BOOLEAN DEFAULT TRUE
);

-- Audit & Logs
CREATE TABLE IF NOT EXISTS AuditLogs (
    LogID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Action VARCHAR(255) NOT NULL,
    ModuleName VARCHAR(100),
    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    IPAddress VARCHAR(45),
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL
);

-- Initial Data
INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Manager'), ('Cashier'), ('Inventory Staff');

INSERT INTO Branches (BranchName, Location, IsHeadOffice) VALUES ('Main Branch', 'Colombo', TRUE);

-- Default Admin User (Username: admin, Password: admin)
-- SHA256 of 'admin' is 8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918
INSERT INTO Users (Username, PasswordHash, RoleID, BranchID, FullName, IsActive)
VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1, 1, 'System Administrator', 1);

INSERT INTO Categories (CategoryName) VALUES ('Beverages'), ('Groceries'), ('Frozen Foods'), ('Cosmetics'), ('Vegetables'), ('Electronics'), ('Bakery'), ('Household');

INSERT INTO TaxCategories (TaxName, TaxPercentage) VALUES ('Standard VAT', 15.00), ('Luxury Tax', 18.00), ('Zero Rated', 0.00);
