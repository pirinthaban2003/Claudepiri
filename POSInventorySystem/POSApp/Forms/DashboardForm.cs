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
        private readonly NotificationService _notificationService;

        public DashboardForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _notificationService = new NotificationService();
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
            lstAlerts.BackColor = ThemeHelper.PrimaryDark;
            lstAlerts.ForeColor = ThemeHelper.TextWhite;

            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(20, 0, 0, 0);
                }
            }

            ApplyAccessControl();
        }

        private void ApplyAccessControl()
        {
            string role = Session.CurrentUser?.RoleName ?? "";
            btnPOS.Visible = AccessControl.CanAccess(role, "POS");
            btnInventory.Visible = AccessControl.CanAccess(role, "Inventory");
            btnSuppliers.Visible = AccessControl.CanAccess(role, "Suppliers");
            btnCustomers.Visible = AccessControl.CanAccess(role, "Customers");
            btnReports.Visible = AccessControl.CanAccess(role, "Reports");
            btnExpenses.Visible = AccessControl.CanAccess(role, "Expenses");
            btnReturns.Visible = AccessControl.CanAccess(role, "Returns");
        }

        private void DashboardForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.P && btnPOS.Visible)
            {
                btnPOS.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.I && btnInventory.Visible)
            {
                btnInventory.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.S && btnSuppliers.Visible)
            {
                btnSuppliers.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.C && btnCustomers.Visible)
            {
                btnCustomers.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.R && btnReports.Visible)
            {
                btnReports.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.E && btnExpenses.Visible)
            {
                btnExpenses.PerformClick();
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.F && btnReturns.Visible)
            {
                btnReturns.PerformClick();
                e.Handled = true;
            }
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentUser?.FullName ?? Session.CurrentUser?.Username ?? "User"}!";
            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            LoadStats();
            LoadLiveAlerts();
        }

        private void LoadStats()
        {
            try
            {
                var salesToday = _dbHelper.ExecuteScalar("SELECT SUM(FinalAmount) FROM Sales WHERE DATE(SaleDate) = CURDATE()");
                lblSalesTodayAmount.Text = (salesToday != DBNull.Value ? Convert.ToDecimal(salesToday) : 0).ToString("C");

                var lowStock = _dbHelper.ExecuteScalar("SELECT COUNT(*) FROM Products WHERE StockQuantity <= MinStockLevel AND IsActive = 1");
                lblLowStockCount.Text = (lowStock != DBNull.Value ? Convert.ToInt32(lowStock) : 0).ToString();
            }
            catch { /* Silent stats error */ }
        }

        private void LoadLiveAlerts()
        {
            try
            {
                lstAlerts.Items.Clear();
                var alerts = _notificationService.GetLiveAlerts();
                foreach (var alert in alerts)
                {
                    lstAlerts.Items.Add(alert);
                }
            }
            catch { /* Silent alerts error */ }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshDashboard();
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
