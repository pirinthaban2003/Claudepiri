using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Data;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class ReportForm : Form
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ReportService _reportService;

        public ReportForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _reportService = new ReportService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(ReportForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            AddExtraButtons();
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            lblReportTitle.ForeColor = ThemeHelper.AccentBlue;
        }

        private void ReportForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnDailySales.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnLowStock.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnTopProducts.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                // Trigger Export
                var btn = this.Controls.Find("btnExport", true).FirstOrDefault() as Button;
                btn?.PerformClick();
                e.Handled = true;
            }
        }

        private void AddExtraButtons()
        {
            Button btnProfit = new Button();
            btnProfit.Location = new System.Drawing.Point(410, 60);
            btnProfit.Name = "btnProfit";
            btnProfit.Size = new System.Drawing.Size(120, 30);
            btnProfit.TabIndex = 5;
            btnProfit.Text = "Profit/Loss";
            btnProfit.UseVisualStyleBackColor = true;
            btnProfit.Click += new System.EventHandler(this.btnProfit_Click);
            this.Controls.Add(btnProfit);

            Button btnExport = new Button();
            btnExport.Location = new System.Drawing.Point(660, 60);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(120, 30);
            btnExport.TabIndex = 6;
            btnExport.Text = "Export (Ctrl+E)";
            btnExport.BackColor = ThemeHelper.AccentGreen;
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.Controls.Add(btnExport);
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT DATE(SaleDate) as Date, COUNT(SaleID) as Transactions, SUM(FinalAmount) as Revenue
                                FROM Sales
                                GROUP BY DATE(SaleDate)
                                ORDER BY Date DESC";
                dgvReports.DataSource = _dbHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Daily Sales report: " + ex.Message);
            }
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT SKU, ProductName, StockQuantity, MinStockLevel FROM Products WHERE StockQuantity <= MinStockLevel AND IsActive = 1";
                dgvReports.DataSource = _dbHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Low Stock report: " + ex.Message);
            }
        }

        private void btnTopProducts_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT p.ProductName, SUM(si.Quantity) as TotalSold
                                FROM SaleItems si
                                JOIN Products p ON si.ProductID = p.ProductID
                                GROUP BY p.ProductID
                                ORDER BY TotalSold DESC
                                LIMIT 10";
                dgvReports.DataSource = _dbHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Top Products report: " + ex.Message);
            }
        }

        private void btnProfit_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                    SELECT
                        (SELECT IFNULL(SUM(FinalAmount), 0) FROM Sales) as TotalRevenue,
                        (SELECT IFNULL(SUM(CostPrice * InitialQuantity), 0) FROM InventoryBatches) as TotalCOGS,
                        (SELECT IFNULL(SUM(Amount), 0) FROM Expenses) as TotalExpenses,
                        ((SELECT IFNULL(SUM(FinalAmount), 0) FROM Sales) -
                         (SELECT IFNULL(SUM(CostPrice * InitialQuantity), 0) FROM InventoryBatches) -
                         (SELECT IFNULL(SUM(Amount), 0) FROM Expenses)) as NetProfit";
                dgvReports.DataSource = _dbHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Profit/Loss report: " + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvReports.DataSource == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (_reportService.ExportToCSV((DataTable)dgvReports.DataSource, sfd.FileName))
                    {
                        MessageBox.Show("Report exported successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to export report.");
                    }
                }
            }
        }
    }
}
