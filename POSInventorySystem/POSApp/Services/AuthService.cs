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
            MySqlParameter[] parameters = { new MySqlParameter("@username", username) };
            DataTable dt = _dbHelper.ExecuteQuery("SELECT u.*, r.RoleName FROM Users u JOIN Roles r ON u.RoleID = r.RoleID WHERE u.Username = @username AND u.IsActive = 1", parameters);

            if (dt.Rows.Count > 0)
            {
                string storedHash = dt.Rows[0]["PasswordHash"].ToString() ?? "";
                if (SecurityHelper.VerifyPassword(password, storedHash))
                {
                    var user = new User
                    {
                        UserID = Convert.ToInt32(dt.Rows[0]["UserID"]),
                        Username = dt.Rows[0]["Username"].ToString() ?? "",
                        RoleID = Convert.ToInt32(dt.Rows[0]["RoleID"]),
                        RoleName = dt.Rows[0]["RoleName"].ToString() ?? "",
                        FullName = dt.Rows[0]["FullName"]?.ToString(),
                        Email = dt.Rows[0]["Email"]?.ToString(),
                        IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]),
                        CreatedAt = Convert.ToDateTime(dt.Rows[0]["CreatedAt"])
                    };

                    _auditService.LogAction("User Login", "Authentication");
                    return user;
                }
            }

            _auditService.LogAction($"Failed Login Attempt: {username}", "Authentication");
            return null;
        }
    }
}
