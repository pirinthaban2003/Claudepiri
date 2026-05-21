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

                            // Update Stock using FIFO Logic (Deduct from oldest batches first)
                            int remainingQty = item.Quantity;

                            string batchQuery = "SELECT BatchID, Quantity FROM InventoryBatches WHERE ProductID = @prodId AND Quantity > 0 ORDER BY ReceivedDate ASC";
                            MySqlCommand batchCmd = new MySqlCommand(batchQuery, conn, trans);
                            batchCmd.Parameters.AddWithValue("@prodId", item.ProductID);

                            using (MySqlDataReader reader = batchCmd.ExecuteReader())
                            {
                                List<(int BatchID, int Quantity)> batches = new List<(int, int)>();
                                while (reader.Read())
                                {
                                    batches.Add((reader.GetInt32(0), reader.GetInt32(1)));
                                }
                                reader.Close();

                                foreach (var batch in batches)
                                {
                                    if (remainingQty <= 0) break;

                                    int deductQty = Math.Min(remainingQty, batch.Quantity);

                                    string updateBatchQuery = "UPDATE InventoryBatches SET Quantity = Quantity - @qty WHERE BatchID = @batchId";
                                    MySqlCommand updateBatchCmd = new MySqlCommand(updateBatchQuery, conn, trans);
                                    updateBatchCmd.Parameters.AddWithValue("@qty", deductQty);
                                    updateBatchCmd.Parameters.AddWithValue("@batchId", batch.BatchID);
                                    updateBatchCmd.ExecuteNonQuery();

                                    remainingQty -= deductQty;
                                }
                            }

                            // Update overall product stock
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

                                // Update Level within transaction
                                var levelCheckQuery = "SELECT LoyaltyPoints FROM Customers WHERE CustomerID = @id";
                                MySqlCommand levelCheckCmd = new MySqlCommand(levelCheckQuery, conn, trans);
                                levelCheckCmd.Parameters.AddWithValue("@id", sale.CustomerID.Value);
                                int totalPoints = Convert.ToInt32(levelCheckCmd.ExecuteScalar());

                                string level = "Bronze";
                                if (totalPoints >= 5000) level = "Gold";
                                else if (totalPoints >= 1000) level = "Silver";

                                string updateLevelQuery = "UPDATE Customers SET LoyaltyLevel = @level WHERE CustomerID = @id";
                                MySqlCommand updateLevelCmd = new MySqlCommand(updateLevelQuery, conn, trans);
                                updateLevelCmd.Parameters.AddWithValue("@level", level);
                                updateLevelCmd.Parameters.AddWithValue("@id", sale.CustomerID.Value);
                                updateLevelCmd.ExecuteNonQuery();
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
