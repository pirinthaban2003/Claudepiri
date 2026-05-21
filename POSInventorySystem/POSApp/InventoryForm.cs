using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp
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

        private void LoadProducts()
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery(@"
                    SELECT p.ProductID, p.ProductName, c.CategoryName, p.Price, p.StockQuantity, p.Barcode, p.CategoryID
                    FROM Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID");
                dgvProducts.DataSource = dt;
                if (dgvProducts.Columns["CategoryID"] != null)
                    dgvProducts.Columns["CategoryID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please fill in required fields.");
                return;
            }

            try
            {
                MySqlParameter[] parameters = {
                    new MySqlParameter("@name", txtProductName.Text),
                    new MySqlParameter("@catId", cmbCategory.SelectedValue),
                    new MySqlParameter("@price", decimal.Parse(txtPrice.Text)),
                    new MySqlParameter("@qty", int.Parse(txtQuantity.Text)),
                    new MySqlParameter("@id", selectedProductId)
                };

                if (selectedProductId == -1)
                {
                    dbHelper.ExecuteNonQuery("INSERT INTO Products (ProductName, CategoryID, Price, StockQuantity) VALUES (@name, @catId, @price, @qty)", parameters);
                }
                else
                {
                    dbHelper.ExecuteNonQuery("UPDATE Products SET ProductName=@name, CategoryID=@catId, Price=@price, StockQuantity=@qty WHERE ProductID=@id", parameters);
                }

                ClearFields();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1) return;

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    MessageBox.Show("Error deleting product: " + ex.Message);
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
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtQuantity.Text = row.Cells["StockQuantity"].Value.ToString();
                cmbCategory.SelectedValue = row.Cells["CategoryID"].Value;
                btnSave.Text = "Update";
            }
        }
    }
}
