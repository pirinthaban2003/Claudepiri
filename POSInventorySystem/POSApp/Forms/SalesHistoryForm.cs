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

        public SalesHistoryForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _saleService = new SaleService();
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnSearch.BackColor = ThemeHelper.AccentBlue;
            btnRefresh.BackColor = ThemeHelper.AccentGreen;
        }

        private void SalesHistoryForm_Load(object sender, EventArgs e)
        {
            LoadSales();
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

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
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

    partial class SalesHistoryForm
    {
        private System.Windows.Forms.DataGridView dgvSales = new System.Windows.Forms.DataGridView();
        private System.Windows.Forms.DataGridView dgvDetails = new System.Windows.Forms.DataGridView();
        private System.Windows.Forms.TextBox txtSearchID = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.Button btnSearch = new System.Windows.Forms.Button();
        private System.Windows.Forms.Button btnRefresh = new System.Windows.Forms.Button();
        private System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblDetailTitle = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel pnlSearch = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Button btnBack = new System.Windows.Forms.Button();

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "Sales Records";

            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.btnBack.Text = "←";
            this.btnBack.Location = new Point(5, 5);
            this.btnBack.Size = new Size(40, 25);
            this.btnBack.Click += new EventHandler(btnBack_Click);
            this.Controls.Add(btnBack);

            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchID);
            this.pnlSearch.Location = new System.Drawing.Point(20, 60);
            this.pnlSearch.Size = new System.Drawing.Size(400, 40);

            this.txtSearchID.Location = new Point(0, 5);
            this.txtSearchID.Width = 150;
            this.txtSearchID.PlaceholderText = "Enter Sale ID";

            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.Location = new Point(160, 4);
            this.btnSearch.Width = 80;
            this.btnSearch.Click += new EventHandler(btnSearch_Click);

            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.Location = new Point(250, 4);
            this.btnRefresh.Width = 80;
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);

            this.dgvSales.Location = new Point(20, 110);
            this.dgvSales.Size = new Size(800, 250);
            this.dgvSales.ReadOnly = true;
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.CellClick += new DataGridViewCellEventHandler(dgvSales_CellClick);

            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new Point(20, 380);
            this.lblDetailTitle.Text = "Sale Details";

            this.dgvDetails.Location = new Point(20, 410);
            this.dgvDetails.Size = new Size(800, 200);
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.ClientSize = new Size(850, 650);
            this.Controls.Add(lblTitle);
            this.Controls.Add(pnlSearch);
            this.Controls.Add(dgvSales);
            this.Controls.Add(lblDetailTitle);
            this.Controls.Add(dgvDetails);
            this.Text = "Sales History";
            this.Load += new EventHandler(SalesHistoryForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
