using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;
using POSApp.Forms;

namespace POSApp.Services
{
    public class ReturnService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;

        public ReturnService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
        }

        public DataTable GetSaleDetails(int saleId)
        {
            string query = @"SELECT si.*, p.ProductName
                            FROM SaleItems si
                            JOIN Products p ON si.ProductID = p.ProductID
                            WHERE si.SaleID = @id";
            return _dbHelper.ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@id", saleId) });
        }

        public bool ProcessReturn(int saleId, List<ReturnItem> items, string reason)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        decimal totalRefund = items.Sum(i => i.RefundAmount);

                        // Insert Return
                        string returnQuery = @"INSERT INTO Returns (SaleID, UserID, TotalRefundAmount, Reason)
                                             VALUES (@saleId, @userId, @total, @reason);
                                             SELECT LAST_INSERT_ID();";
                        MySqlCommand returnCmd = new MySqlCommand(returnQuery, conn, trans);
                        returnCmd.Parameters.AddWithValue("@saleId", saleId);
                        returnCmd.Parameters.AddWithValue("@userId", (object?)Session.CurrentUser?.UserID ?? DBNull.Value);
                        returnCmd.Parameters.AddWithValue("@total", totalRefund);
                        returnCmd.Parameters.AddWithValue("@reason", reason);
                        int returnId = Convert.ToInt32(returnCmd.ExecuteScalar());

                        foreach (var item in items)
                        {
                            // Insert Return Item
                            string itemQuery = "INSERT INTO ReturnItems (ReturnID, ProductID, Quantity, RefundAmount) VALUES (@returnId, @prodId, @qty, @amount)";
                            MySqlCommand itemCmd = new MySqlCommand(itemQuery, conn, trans);
                            itemCmd.Parameters.AddWithValue("@returnId", returnId);
                            itemCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            itemCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@amount", item.RefundAmount);
                            itemCmd.ExecuteNonQuery();

                            // Restock Inventory
                            string restockQuery = "UPDATE Products SET StockQuantity = StockQuantity + @qty WHERE ProductID = @prodId";
                            MySqlCommand restockCmd = new MySqlCommand(restockQuery, conn, trans);
                            restockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            restockCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            restockCmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        _auditService.LogAction($"Processed Return ID: {returnId} for Sale: {saleId}", "Returns");
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }

    public class ReturnItem
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal RefundAmount { get; set; }
    }
}
