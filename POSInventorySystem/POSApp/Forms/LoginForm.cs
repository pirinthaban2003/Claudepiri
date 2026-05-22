using System;
using System.Windows.Forms;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlMain.BackColor = ThemeHelper.PrimaryDark;
            btnLogin.BackColor = ThemeHelper.AccentBlue;
            lblTitle.ForeColor = ThemeHelper.AccentBlue;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            lblTitle.Text = "SRMS 🏪";
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            btnLogin.Enabled = false;

            try
            {
                var user = _authService.Authenticate(username, password);

                if (user != null)
                {
                    Session.CurrentUser = user;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string message = "Access Denied: Invalid username or password.\n\n" +
                                   "Note: If this is your first time, the default credentials are:\n" +
                                   "Username: admin\n" +
                                   "Password: admin\n\n" +
                                   "Please ensure you have run 'database_setup.sql' in your MySQL server.";
                    MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException mex)
            {
                MessageBox.Show($"Database Connection Error: {mex.Message}\n\nPlease ensure your MySQL server is running and the connection string in Data/Configuration.cs is correct.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred during login: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnLogin.Enabled = true;
            }
        }
    }

    public static class Session
    {
        public static Models.User? CurrentUser { get; set; }
    }
}
