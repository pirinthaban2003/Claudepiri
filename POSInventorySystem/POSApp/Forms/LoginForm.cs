using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper dbHelper;

        public LoginForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                MySqlParameter[] parameters = { new MySqlParameter("@username", username) };
                DataTable dt = dbHelper.ExecuteQuery("SELECT u.*, r.RoleName FROM Users u JOIN Roles r ON u.RoleID = r.RoleID WHERE u.Username = @username AND u.IsActive = 1", parameters);

                if (dt.Rows.Count > 0)
                {
                    string storedHash = dt.Rows[0]["PasswordHash"].ToString() ?? "";
                    if (SecurityHelper.VerifyPassword(password, storedHash))
                    {
                        // Successful login
                        Session.CurrentUser = new Models.User
                        {
                            UserID = Convert.ToInt32(dt.Rows[0]["UserID"]),
                            Username = dt.Rows[0]["Username"].ToString() ?? "",
                            RoleID = Convert.ToInt32(dt.Rows[0]["RoleID"]),
                            RoleName = dt.Rows[0]["RoleName"].ToString() ?? "",
                            FullName = dt.Rows[0]["FullName"].ToString()
                        };

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User not found or inactive.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static class Session
    {
        public static Models.User? CurrentUser { get; set; }
    }
}
