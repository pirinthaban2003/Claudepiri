using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Data;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly DatabaseHelper _dbHelper;

        public DashboardForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(DashboardForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlSidebar.BackColor = ThemeHelper.PrimaryDark;
            pnlHeader.BackColor = ThemeHelper.SecondaryDark;
            lblTitle.ForeColor = ThemeHelper.AccentBlue;

            // Sidebar buttons hover effect could be added here in a real environment
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(20, 0, 0, 0);
                }
            }
        }

        private void DashboardForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.P)
            {
                btnPOS.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.I)
            {
                btnInventory.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.S)
            {
                btnSuppliers.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                btnCustomers.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.R)
            {
                btnReports.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.E)
            {
                btnExpenses.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.F)
            {
                btnReturns.PerformClick();
                e.Handled = true;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentUser?.FullName ?? Session.CurrentUser?.Username ?? "User"}!";
            LoadStats();
            CheckExpiries();
        }

        private void LoadStats()
        {
            try
            {
                // Sales Today
                var salesToday = _dbHelper.ExecuteScalar("SELECT SUM(FinalAmount) FROM Sales WHERE DATE(SaleDate) = CURDATE()");
                lblSalesTodayAmount.Text = (salesToday != DBNull.Value ? Convert.ToDecimal(salesToday) : 0).ToString("C");

                // Low Stock Items
                var lowStock = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Products WHERE StockQuantity <= MinStockLevel AND IsActive = 1");
                lblLowStockCount.Text = (lowStock != DBNull.Value ? Convert.ToInt32(lowStock) : 0).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard stats: " + ex.Message);
            }
        }

        private void CheckExpiries()
        {
            try
            {
                var expiringSoon = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM InventoryBatches WHERE ExpiryDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY) AND Quantity > 0");
                int count = (expiringSoon != DBNull.Value ? Convert.ToInt32(expiringSoon) : 0);
                if (count > 0)
                {
                    MessageBox.Show($"{count} product(s) are expiring within the next 7 days!", "Expiry Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { /* Ignore alert errors */ }
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            new POSForm().Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            new InventoryForm().Show();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            new SupplierForm().Show();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            new CustomerForm().Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ReportForm().Show();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            new ExpenseForm().Show();
        }

        private void btnReturns_Click(object sender, EventArgs e)
        {
            new RefundForm().Show();
        }
    }
}
