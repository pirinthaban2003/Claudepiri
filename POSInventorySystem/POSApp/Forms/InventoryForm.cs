using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Forms
{
    public partial class InventoryForm : Form
    {
        private DatabaseHelper dbHelper;
        private int selectedProductId = -1;

        public InventoryForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery("SELECT * FROM Categories");
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery("SELECT * FROM Suppliers");
                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
        }

        private void LoadProducts()
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery(@"
                    SELECT p.*, c.CategoryName, s.SupplierName
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    LEFT JOIN Suppliers s ON p.SupplierID = s.SupplierID");
                dgvProducts.DataSource = dt;

                // Hide ID columns
                string[] hideCols = { "CategoryID", "SupplierID" };
                foreach (string col in hideCols)
                {
                    if (dgvProducts.Columns[col] != null)
                        dgvProducts.Columns[col].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price. Please enter a numeric value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Invalid quantity. Please enter an integer value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                MySqlParameter[] parameters = {
                    new MySqlParameter("@name", txtProductName.Text.Trim()),
                    new MySqlParameter("@sku", txtSKU.Text.Trim()),
                    new MySqlParameter("@barcode", txtBarcode.Text.Trim()),
                    new MySqlParameter("@catId", cmbCategory.SelectedValue),
                    new MySqlParameter("@supId", cmbSupplier.SelectedValue),
                    new MySqlParameter("@brand", txtBrand.Text.Trim()),
                    new MySqlParameter("@price", price),
                    new MySqlParameter("@qty", quantity),
                    new MySqlParameter("@id", selectedProductId)
                };

                if (selectedProductId == -1)
                {
                    string query = @"INSERT INTO Products (ProductName, SKU, Barcode, CategoryID, SupplierID, Brand, Price, StockQuantity)
                                   VALUES (@name, @sku, @barcode, @catId, @supId, @brand, @price, @qty)";
                    dbHelper.ExecuteNonQuery(query, parameters);
                }
                else
                {
                    string query = @"UPDATE Products SET ProductName=@name, SKU=@sku, Barcode=@barcode, CategoryID=@catId,
                                   SupplierID=@supId, Brand=@brand, Price=@price, StockQuantity=@qty WHERE ProductID=@id";
                    dbHelper.ExecuteNonQuery(query, parameters);
                }

                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1) return;

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    MySqlParameter[] parameters = { new MySqlParameter("@id", selectedProductId) };
                    dbHelper.ExecuteNonQuery("DELETE FROM Products WHERE ProductID=@id", parameters);
                    ClearFields();
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtSKU.Clear();
            txtBarcode.Clear();
            txtBrand.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            selectedProductId = -1;
            btnSave.Text = "Save";
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
                selectedProductId = Convert.ToInt32(row.Cells["ProductID"].Value);
                txtProductName.Text = row.Cells["ProductName"].Value?.ToString();
                txtSKU.Text = row.Cells["SKU"].Value?.ToString();
                txtBarcode.Text = row.Cells["Barcode"].Value?.ToString();
                txtBrand.Text = row.Cells["Brand"].Value?.ToString();
                txtPrice.Text = row.Cells["Price"].Value?.ToString();
                txtQuantity.Text = row.Cells["StockQuantity"].Value?.ToString();

                if (row.Cells["CategoryID"].Value != DBNull.Value)
                    cmbCategory.SelectedValue = row.Cells["CategoryID"].Value;

                if (row.Cells["SupplierID"].Value != DBNull.Value)
                    cmbSupplier.SelectedValue = row.Cells["SupplierID"].Value;

                btnSave.Text = "Update";
            }
        }
    }
}
