using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Data;
using POSApp.Services;

namespace POSApp.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly DatabaseHelper _dbHelper;

        public DashboardForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
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
