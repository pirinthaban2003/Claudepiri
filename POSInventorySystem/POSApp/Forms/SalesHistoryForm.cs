using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using POSApp.Data;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class SalesHistoryForm : Form
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly SaleService _saleService;
        private int _initialCustomerID = -1;

        public SalesHistoryForm(int customerID = -1)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _saleService = new SaleService();
            _initialCustomerID = customerID;
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnSearch.BackColor = ThemeHelper.AccentBlue;
            btnRefresh.BackColor = ThemeHelper.AccentGreen;
        }

        private void SalesHistoryForm_Load(object? sender, EventArgs e)
        {
            if (_initialCustomerID != -1)
            {
                LoadSalesByCustomer(_initialCustomerID);
            }
            else
            {
                LoadSales();
            }
        }

        private void LoadSalesByCustomer(int customerId)
        {
            try
            {
                string query = @"
                    SELECT s.SaleID, s.SaleDate, IFNULL(c.CustomerName, 'Walk-in') as Customer,
                           s.TotalAmount, s.DiscountAmount, s.TaxAmount, s.FinalAmount, u.Username as Cashier
                    FROM Sales s
                    LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                    LEFT JOIN Users u ON s.UserID = u.UserID
                    WHERE s.CustomerID = @customerId
                    ORDER BY s.SaleDate DESC";
                MySql.Data.MySqlClient.MySqlParameter[] parameters = { new MySql.Data.MySqlClient.MySqlParameter("@customerId", customerId) };
                dgvSales.DataSource = _dbHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customer sales: " + ex.Message);
            }
        }

        private void LoadSales(string search = "")
        {
            try
            {
                string query = @"
                    SELECT s.SaleID, s.SaleDate, IFNULL(c.CustomerName, 'Walk-in') as Customer,
                           s.TotalAmount, s.DiscountAmount, s.TaxAmount, s.FinalAmount, u.Username as Cashier
                    FROM Sales s
                    LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                    LEFT JOIN Users u ON s.UserID = u.UserID";

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query += " WHERE s.SaleID = @id";
                    MySql.Data.MySqlClient.MySqlParameter[] parameters = { new MySql.Data.MySqlClient.MySqlParameter("@id", search) };
                    dgvSales.DataSource = _dbHelper.ExecuteQuery(query, parameters);
                }
                else
                {
                    query += " ORDER BY s.SaleDate DESC LIMIT 100";
                    dgvSales.DataSource = _dbHelper.ExecuteQuery(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales: " + ex.Message);
            }
        }

        private void dgvSales_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var saleId = dgvSales.Rows[e.RowIndex].Cells["SaleID"].Value;
                if (saleId != null)
                {
                    LoadSaleDetails(Convert.ToInt32(saleId));
                }
            }
        }

        private void LoadSaleDetails(int saleId)
        {
            try
            {
                string query = @"
                    SELECT p.ProductName, si.Quantity, si.UnitPrice, si.Discount, si.Subtotal
                    FROM SaleItems si
                    JOIN Products p ON si.ProductID = p.ProductID
                    WHERE si.SaleID = @id";
                MySql.Data.MySqlClient.MySqlParameter[] parameters = { new MySql.Data.MySqlClient.MySqlParameter("@id", saleId) };
                dgvDetails.DataSource = _dbHelper.ExecuteQuery(query, parameters);
                lblDetailTitle.Text = "Items for Sale #" + saleId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message);
            }
        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            LoadSales(txtSearchID.Text.Trim());
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            txtSearchID.Clear();
            LoadSales();
        }
    }
}
