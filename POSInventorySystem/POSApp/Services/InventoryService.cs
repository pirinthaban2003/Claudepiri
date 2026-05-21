using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Services
{
    public class InventoryService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;

        public InventoryService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
        }

        public DataTable GetCategories()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Categories");
        }

        public DataTable GetAllProducts()
        {
            return _dbHelper.ExecuteQuery(@"
                SELECT p.*, c.CategoryName, s.SupplierName
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID");
        }

        public int SaveProduct(Product product)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("@name", product.ProductName),
                new MySqlParameter("@sku", product.SKU ?? (object)DBNull.Value),
                new MySqlParameter("@barcode", product.Barcode ?? (object)DBNull.Value),
                new MySqlParameter("@catId", product.CategoryID ?? (object)DBNull.Value),
                new MySqlParameter("@supId", product.SupplierID ?? (object)DBNull.Value),
                new MySqlParameter("@brand", product.Brand ?? (object)DBNull.Value),
                new MySqlParameter("@price", product.Price),
                new MySqlParameter("@qty", product.StockQuantity),
                new MySqlParameter("@id", product.ProductID)
            };

            int result;
            if (product.ProductID == -1 || product.ProductID == 0)
            {
                string query = @"INSERT INTO Products (ProductName, SKU, Barcode, CategoryID, SupplierID, Brand, Price, StockQuantity)
                               VALUES (@name, @sku, @barcode, @catId, @supId, @brand, @price, @qty)";
                result = _dbHelper.ExecuteNonQuery(query, parameters);
                _auditService.LogAction($"Added Product: {product.ProductName}", "Inventory");
            }
            else
            {
                string query = @"UPDATE Products SET ProductName=@name, SKU=@sku, Barcode=@barcode, CategoryID=@catId,
                               SupplierID=@supId, Brand=@brand, Price=@price, StockQuantity=@qty WHERE ProductID=@id";
                result = _dbHelper.ExecuteNonQuery(query, parameters);
                _auditService.LogAction($"Updated Product: {product.ProductName}", "Inventory");
            }
            return result;
        }

        public int DeleteProduct(int productId)
        {
            MySqlParameter[] parameters = { new MySqlParameter("@id", productId) };
            int result = _dbHelper.ExecuteNonQuery("DELETE FROM Products WHERE ProductID=@id", parameters);
            _auditService.LogAction($"Deleted Product ID: {productId}", "Inventory");
            return result;
        }
    }
}
