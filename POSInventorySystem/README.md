# POS and Inventory System

A simple Point of Sale (POS) and Inventory Management system built with C# Windows Forms and MySQL.

## Features

- **Inventory Management**: Add, update, and delete products. Manage stock levels and categories.
- **Point of Sale (POS)**: Process sales by adding products to a cart, calculating totals, and finalizing transactions.
- **Database Persistence**: All data is stored in a MySQL database.

## Prerequisites

- .NET 10.0 SDK
- MySQL Server

## Database Setup

1. Open your MySQL client (e.g., MySQL Workbench or command line).
2. Run the script provided in `database_setup.sql` to create the `pos_db` database and its tables.

```sql
SOURCE path/to/database_setup.sql;
```

## Configuration

Update the connection string in `InventoryForm.cs` and `POSForm.cs` if your MySQL server uses different credentials:

```csharp
dbHelper = new DatabaseHelper("localhost", "pos_db", "root", "your_password");
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

- `Form1.cs`: Main menu and navigation.
- `InventoryForm.cs`: Product and stock management.
- `POSForm.cs`: Sales processing.
- `DatabaseHelper.cs`: MySQL connection and query execution helper.
- `Models.cs`: Data entities (Product, Category, Sale, SaleItem).
