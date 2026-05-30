using System.Collections.Generic;
using System.Data;
using POSApp.Data;

namespace POSApp.Services
{
    public class NotificationService
    {
        private readonly DatabaseHelper _dbHelper;

        public NotificationService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public List<string> GetLiveAlerts()
        {
            List<string> alerts = new List<string>();

            // 1. Low Stock Alerts
            DataTable lowStock = _dbHelper.ExecuteQuery("SELECT ProductName FROM Products WHERE StockQuantity <= MinStockLevel AND IsActive = 1");
            foreach (DataRow row in lowStock.Rows)
            {
                alerts.Add($"LOW STOCK: {row["ProductName"]}");
            }

            // 2. Expiry Alerts (7 days)
            DataTable expiries = _dbHelper.ExecuteQuery(@"
                SELECT p.ProductName, b.BatchNumber
                FROM InventoryBatches b
                JOIN Products p ON b.ProductID = p.ProductID
                WHERE b.ExpiryDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)
                AND b.Quantity > 0");
            foreach (DataRow row in expiries.Rows)
            {
                alerts.Add($"EXPIRING SOON: {row["ProductName"]} (Batch: {row["BatchNumber"]})");
            }

            // 3. Recent Failed Logins (Security)
            DataTable failedLogins = _dbHelper.ExecuteQuery(@"
                SELECT Action FROM AuditLogs
                WHERE Action LIKE 'Failed Login Attempt%'
                AND Timestamp >= DATE_SUB(NOW(), INTERVAL 1 HOUR)");
            foreach (DataRow row in failedLogins.Rows)
            {
                alerts.Add($"SECURITY: {row["Action"]}");
            }

            return alerts;
        }
    }
}
