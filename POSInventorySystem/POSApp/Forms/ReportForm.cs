using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Data;

namespace POSApp.Forms
{
    public partial class ReportForm : Form
    {
        private readonly DatabaseHelper _dbHelper;

        public ReportForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
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
    }
}
