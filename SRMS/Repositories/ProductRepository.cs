using MySql.Data.MySqlClient;
using SRMS.Database;
using SRMS.Models;

namespace SRMS.Repositories
{
    public class ProductRepository
    {
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();
            using var connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM products", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                    ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                    Barcode = reader.IsDBNull(reader.GetOrdinal("barcode")) ? null : reader.GetString(reader.GetOrdinal("barcode")),
                    SKU = reader.IsDBNull(reader.GetOrdinal("sku")) ? null : reader.GetString(reader.GetOrdinal("sku")),
                    CategoryId = reader.IsDBNull(reader.GetOrdinal("category_id")) ? null : reader.GetInt32(reader.GetOrdinal("category_id")),
                    BrandId = reader.IsDBNull(reader.GetOrdinal("brand_id")) ? null : reader.GetInt32(reader.GetOrdinal("brand_id")),
                    Unit = reader.IsDBNull(reader.GetOrdinal("unit")) ? null : reader.GetString(reader.GetOrdinal("unit")),
                    CostPrice = reader.GetDecimal(reader.GetOrdinal("cost_price")),
                    SellingPrice = reader.GetDecimal(reader.GetOrdinal("selling_price")),
                    TaxRate = reader.GetDecimal(reader.GetOrdinal("tax_rate")),
                    Status = reader.GetString(reader.GetOrdinal("status")),
                    ImagePath = reader.IsDBNull(reader.GetOrdinal("image_path")) ? null : reader.GetString(reader.GetOrdinal("image_path"))
                });
            }
            return products;
        }
    }
}
