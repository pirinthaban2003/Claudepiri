using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;
using POSApp.Utilities;

namespace POSApp.Services
{
    public class AuthService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;

        public AuthService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
        }

        public User? Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            try
            {
                // Trim inputs to handle accidental spaces
                string cleanUsername = username.Trim().ToLower();
                string cleanPassword = password.Trim();

                MySqlParameter[] parameters = { new MySqlParameter("@username", cleanUsername) };
                string query = @"
                    SELECT u.*, r.RoleName, b.BranchName
                    FROM Users u
                    JOIN Roles r ON u.RoleID = r.RoleID
                    LEFT JOIN Branches b ON u.BranchID = b.BranchID
                    WHERE LOWER(u.Username) = @username AND u.IsActive = 1";

                DataTable dt = _dbHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    string storedHash = dt.Rows[0]["PasswordHash"].ToString() ?? "";
                    if (SecurityHelper.VerifyPassword(cleanPassword, storedHash))
                    {
                        var user = new User
                        {
                            UserID = Convert.ToInt32(dt.Rows[0]["UserID"]),
                            Username = dt.Rows[0]["Username"].ToString() ?? "",
                            RoleID = Convert.ToInt32(dt.Rows[0]["RoleID"]),
                            RoleName = dt.Rows[0]["RoleName"].ToString() ?? "",
                            BranchID = dt.Rows[0]["BranchID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["BranchID"]) : (int?)null,
                            BranchName = dt.Rows[0]["BranchName"]?.ToString(),
                            FullName = dt.Rows[0]["FullName"]?.ToString(),
                            Email = dt.Rows[0]["Email"]?.ToString(),
                            IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]),
                            CreatedAt = Convert.ToDateTime(dt.Rows[0]["CreatedAt"])
                        };

                        _auditService.LogAction("User Login", "Authentication");
                        return user;
                    }
                }

                _auditService.LogAction($"Failed Login Attempt: {cleanUsername}", "Authentication");
                return null;
            }
            catch (Exception ex)
            {
                _auditService.LogAction($"Login Error for {username}: {ex.Message}", "Authentication");
                throw;
            }
        }
    }
}
