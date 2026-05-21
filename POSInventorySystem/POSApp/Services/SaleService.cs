using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;
using POSApp.Forms;

namespace POSApp.Services
{
    public class SaleService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;
        private readonly CustomerService _customerService;

        public SaleService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
            _customerService = new CustomerService();
        }

        public DataTable GetAvailableProducts()
        {
            return _dbHelper.ExecuteQuery("SELECT ProductID, ProductName, Price, StockQuantity FROM Products WHERE StockQuantity > 0");
        }

        public bool ProcessSale(Sale sale)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert Sale
                        string saleQuery = @"INSERT INTO Sales (CustomerID, UserID, TotalAmount, DiscountAmount, TaxAmount, FinalAmount)
                                           VALUES (@custId, @userId, @total, @discount, @tax, @final);
                                           SELECT LAST_INSERT_ID();";
                        MySqlCommand saleCmd = new MySqlCommand(saleQuery, conn, trans);
                        saleCmd.Parameters.AddWithValue("@custId", sale.CustomerID ?? (object)DBNull.Value);
                        saleCmd.Parameters.AddWithValue("@userId", (object?)Session.CurrentUser?.UserID ?? DBNull.Value);
                        saleCmd.Parameters.AddWithValue("@total", sale.TotalAmount);
                        saleCmd.Parameters.AddWithValue("@discount", sale.DiscountAmount);
                        saleCmd.Parameters.AddWithValue("@tax", sale.TaxAmount);
                        saleCmd.Parameters.AddWithValue("@final", sale.FinalAmount);
                        int saleId = Convert.ToInt32(saleCmd.ExecuteScalar());

                        foreach (var item in sale.Items)
                        {
                            // Insert Sale Item
                            string itemQuery = "INSERT INTO SaleItems (SaleID, ProductID, Quantity, UnitPrice, Discount, Subtotal) VALUES (@saleId, @prodId, @qty, @price, @discount, @subtotal)";
                            MySqlCommand itemCmd = new MySqlCommand(itemQuery, conn, trans);
                            itemCmd.Parameters.AddWithValue("@saleId", saleId);
                            itemCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            itemCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@price", item.UnitPrice);
                            itemCmd.Parameters.AddWithValue("@discount", item.Discount);
                            itemCmd.Parameters.AddWithValue("@subtotal", item.Subtotal);
                            itemCmd.ExecuteNonQuery();

                            // Update Stock
                            string stockQuery = "UPDATE Products SET StockQuantity = StockQuantity - @qty WHERE ProductID = @prodId";
                            MySqlCommand stockCmd = new MySqlCommand(stockQuery, conn, trans);
                            stockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            stockCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            stockCmd.ExecuteNonQuery();
                        }

                        // Handle Loyalty Points (1 point for every 100 spent)
                        if (sale.CustomerID.HasValue && sale.FinalAmount > 0)
                        {
                            int pointsEarned = (int)(sale.FinalAmount / 100);
                            if (pointsEarned > 0)
                            {
                                string loyaltyQuery = "UPDATE Customers SET LoyaltyPoints = LoyaltyPoints + @points WHERE CustomerID = @id";
                                MySqlCommand loyaltyCmd = new MySqlCommand(loyaltyQuery, conn, trans);
                                loyaltyCmd.Parameters.AddWithValue("@points", pointsEarned);
                                loyaltyCmd.Parameters.AddWithValue("@id", sale.CustomerID.Value);
                                loyaltyCmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        _auditService.LogAction($"Completed Sale ID: {saleId}", "POS");
                        return true;
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
