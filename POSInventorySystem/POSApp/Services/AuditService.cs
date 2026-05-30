using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Forms;

namespace POSApp.Services
{
    public class AuditService
    {
        private readonly DatabaseHelper _dbHelper;

        public AuditService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public void LogAction(string action, string moduleName)
        {
            try
            {
                string query = "INSERT INTO AuditLogs (UserID, Action, ModuleName) VALUES (@userId, @action, @module)";
                MySqlParameter[] parameters = {
                    new MySqlParameter("@userId", (object?)Session.CurrentUser?.UserID ?? DBNull.Value),
                    new MySqlParameter("@action", action),
                    new MySqlParameter("@module", moduleName)
                };
                _dbHelper.ExecuteNonQuery(query, parameters);
            }
            catch
            {
                // Silence audit logging errors to prevent blocking main functionality
            }
        }
    }
}
