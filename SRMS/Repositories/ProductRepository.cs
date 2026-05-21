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

        public async Task<bool> AddProductAsync(Product product)
        {
            using var connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            await connection.OpenAsync();

            string query = "INSERT INTO products (product_name, barcode, sku, category_id, brand_id, unit, cost_price, selling_price, tax_rate, status) " +
                           "VALUES (@name, @barcode, @sku, @catId, @brandId, @unit, @cost, @selling, @tax, @status)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.ProductName);
            command.Parameters.AddWithValue("@barcode", (object?)product.Barcode ?? DBNull.Value);
            command.Parameters.AddWithValue("@sku", (object?)product.SKU ?? DBNull.Value);
            command.Parameters.AddWithValue("@catId", (object?)product.CategoryId ?? DBNull.Value);
            command.Parameters.AddWithValue("@brandId", (object?)product.BrandId ?? DBNull.Value);
            command.Parameters.AddWithValue("@unit", (object?)product.Unit ?? DBNull.Value);
            command.Parameters.AddWithValue("@cost", product.CostPrice);
            command.Parameters.AddWithValue("@selling", product.SellingPrice);
            command.Parameters.AddWithValue("@tax", product.TaxRate);
            command.Parameters.AddWithValue("@status", product.Status);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            using var connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            await connection.OpenAsync();

            string query = "UPDATE products SET product_name=@name, barcode=@barcode, sku=@sku, category_id=@catId, brand_id=@brandId, " +
                           "unit=@unit, cost_price=@cost, selling_price=@selling, tax_rate=@tax, status=@status WHERE product_id=@id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", product.ProductId);
            command.Parameters.AddWithValue("@name", product.ProductName);
            command.Parameters.AddWithValue("@barcode", (object?)product.Barcode ?? DBNull.Value);
            command.Parameters.AddWithValue("@sku", (object?)product.SKU ?? DBNull.Value);
            command.Parameters.AddWithValue("@catId", (object?)product.CategoryId ?? DBNull.Value);
            command.Parameters.AddWithValue("@brandId", (object?)product.BrandId ?? DBNull.Value);
            command.Parameters.AddWithValue("@unit", (object?)product.Unit ?? DBNull.Value);
            command.Parameters.AddWithValue("@cost", product.CostPrice);
            command.Parameters.AddWithValue("@selling", product.SellingPrice);
            command.Parameters.AddWithValue("@tax", product.TaxRate);
            command.Parameters.AddWithValue("@status", product.Status);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            using var connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("DELETE FROM products WHERE product_id=@id", connection);
            command.Parameters.AddWithValue("@id", productId);

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
