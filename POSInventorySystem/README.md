# Supermarket Retail Management System (SRMS)

A robust Point of Sale (POS) and Inventory Management system built with C# Windows Forms and MySQL, following the SRMS specification.

## Features

- **Authentication & Security**: Role-based access control (Admin, Manager, Cashier, Inventory Staff) with secure login.
- **Product Management**: Track products with SKU, Barcode, Brand, Category, and Supplier mapping.
- **Inventory Management**: Manage stock levels and view detailed product information.
- **Supplier Management**: CRUD operations for suppliers to manage the supply chain.
- **Point of Sale (POS)**: Efficient billing system with cart management, real-time stock updates, and transaction persistence.
- **Database Persistence**: Comprehensive schema covering sales, customers, suppliers, and audit logs.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL Server

## Database Setup

1. Open your MySQL client (e.g., MySQL Workbench or command line).
2. Run the script provided in `database_setup.sql` to create the `pos_db` database and its tables.

```sql
SOURCE path/to/database_setup.sql;
```

## Configuration

Update the connection string in `POSApp/Data/Configuration.cs` if your MySQL server uses different credentials:

```csharp
public static string Server { get; set; } = "localhost";
public static string Database { get; set; } = "pos_db";
public static string User { get; set; } = "root";
public static string Password { get; set; } = "password";
```

## How to Run

1. Navigate to the `POSApp` directory:
   ```bash
   cd POSInventorySystem/POSApp
   ```
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Project Structure

- `Forms/`: UI Windows Forms (Login, Inventory, POS, Supplier).
- `Models/`: Data entities (User, Product, Sale, etc.).
- `Data/`: Database access and configuration.
- `Utilities/`: Security helpers and common utilities.
