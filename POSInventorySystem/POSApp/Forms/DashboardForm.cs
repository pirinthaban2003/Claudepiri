using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private List<decimal> _salesTrend = new List<decimal>();

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
            else if (e.Alt && e.KeyCode == Keys.X)
            {
                btnLogout.PerformClick();
                e.Handled = true;
            }
        }

        private void DashboardForm_Load(object? sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentUser?.FullName ?? Session.CurrentUser?.Username ?? "User"}!";
            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            LoadStats();
            LoadLiveAlerts();
            LoadSalesTrend();
            pnlChart.Invalidate(); // Redraw chart
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

        private void LoadSalesTrend()
        {
            try
            {
                _salesTrend.Clear();
                for (int i = 6; i >= 0; i--)
                {
                    var val = _dbHelper.ExecuteScalar($"SELECT SUM(FinalAmount) FROM Sales WHERE DATE(SaleDate) = DATE_SUB(CURDATE(), INTERVAL {i} DAY)");
                    _salesTrend.Add(val != DBNull.Value ? Convert.ToDecimal(val) : 0);
                }
            }
            catch { }
        }

        private void pnlChart_Paint(object? sender, PaintEventArgs e)
        {
            if (_salesTrend.Count == 0) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pnlChart.Width - 40;
            int height = pnlChart.Height - 60;
            decimal maxSales = _salesTrend.Max();
            if (maxSales == 0) maxSales = 1000;

            int barWidth = width / 7;
            for (int i = 0; i < _salesTrend.Count; i++)
            {
                int barHeight = (int)((_salesTrend[i] / maxSales) * height);
                // Ensure height is at least 1 for drawing if there is any sales, or skip if 0
                if (barHeight <= 0 && _salesTrend[i] > 0) barHeight = 1;

                Rectangle rect = new Rectangle(20 + (i * barWidth) + 10, pnlChart.Height - 40 - barHeight, barWidth - 20, barHeight);

                if (rect.Width > 0 && rect.Height > 0)
                {
                    using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, ThemeHelper.AccentBlue, Color.FromArgb(0, 80, 150), 90F))
                    {
                        g.FillRectangle(brush, rect);
                    }
                }

                g.DrawString(_salesTrend[i].ToString("N0"), this.Font, Brushes.White, rect.X, rect.Y - 20);
                g.DrawString(DateTime.Now.AddDays(i - 6).ToString("dd/MM"), this.Font, Brushes.LightGray, rect.X, pnlChart.Height - 20);
            }
        }

        private void refreshTimer_Tick(object? sender, EventArgs e)
        {
            RefreshDashboard();
        }

        private void btnPOS_Click(object? sender, EventArgs e)
        {
            new POSForm().Show();
        }

        private void btnInventory_Click(object? sender, EventArgs e)
        {
            new InventoryForm().Show();
        }

        private void btnSuppliers_Click(object? sender, EventArgs e)
        {
            new SupplierForm().Show();
        }

        private void btnCustomers_Click(object? sender, EventArgs e)
        {
            new CustomerForm().Show();
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            new ReportForm().Show();
        }

        private void btnExpenses_Click(object? sender, EventArgs e)
        {
            new ExpenseForm().Show();
        }

        private void btnReturns_Click(object? sender, EventArgs e)
        {
            new RefundForm().Show();
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Session.CurrentUser = null;
                this.Hide();
                using (LoginForm login = new LoginForm())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        this.Show();
                        RefreshDashboard();
                        ApplyAccessControl();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }
    }
}
