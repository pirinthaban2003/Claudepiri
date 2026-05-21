# Supermarket Retail Management System (SRMS)

A robust Point of Sale (POS) and Inventory Management system built with C# Windows Forms and MySQL, following the SRMS specification.

## Features

- **Authentication & Security**: Role-based access control (Admin, Manager, Cashier, Inventory Staff) with secure login.
- **Product Management**: Track products with SKU, Barcode, Brand, Category, and Supplier mapping.
- **Inventory Management**: Manage stock levels with FIFO batch tracking and real-time color-coded stock alerts.
- **Supplier & Customer CRM**: Full management for the supply chain and customer loyalty programs.
- **Automated Loyalty**: Tiered progression (Bronze, Silver, Gold) with points added automatically during checkout.
- **Point of Sale (POS)**: High-efficiency terminal with keyboard shortcuts, hold/resume sales, and itemized receipt previews.
- **Enterprise Dashboard**: Real-time visual analytics (GDI+ charts) and live system notifications (Expiries, Security, Low Stock).
- **Business Intelligence**: Detailed reporting for Profit/Loss, Daily Sales, and Top Products with CSV export capabilities.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL Server

## Database Setup

1. Open your MySQL client (e.g., MySQL Workbench or command line).
2. Run the script provided in `database_setup.sql` to create the `pos_db` database and its tables.

```sql
SOURCE path/to/database_setup.sql;
```

## Default Credentials

Use the following credentials to access the system after setup:

- **Username**: `admin`
- **Password**: `admin`

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

## Keyboard Shortcuts

- **F1**: Show All Shortcuts
- **F2**: Focus Barcode Input (POS)
- **F3**: Cash Payment & Checkout (POS)
- **F4**: Hold Sale (POS)
- **F5**: Resume Sale (POS)
- **F10**: Card Payment (POS)
- **F12**: Refresh Data
- **Alt+P**: Go to POS
- **Alt+I**: Go to Inventory
- **Alt+D**: Go to Dashboard
- **Ctrl+F**: Search
- **Enter**: Add Product / Process Payment (Contextual)

## Project Structure

- `Forms/`: UI Windows Forms (Dashboard, Login, Inventory, POS, etc.).
- `Models/`: Data entities (User, Product, Sale, Branch, etc.).
- `Services/`: Business logic layer (Sale, Inventory, Auth, Notification, etc.).
- `Data/`: Database access and configuration.
- `Utilities/`: Theme management, Access Control, and Security helpers.
