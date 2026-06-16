using SRMS.Authentication;
using SRMS.Utilities;

namespace SRMS.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UIStyle.ApplyStyle(this);
            lblWelcome.Text = $"Welcome, {SessionManager.CurrentUser?.Username ?? "User"}";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            Application.Restart();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            using (var productForm = new ProductForm())
            {
                productForm.ShowDialog();
            }
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            MessageBox.Show("POS Module coming soon!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reports Module coming soon!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
