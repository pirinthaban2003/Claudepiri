using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryService _inventoryService;
        private readonly SupplierService _supplierService;
        private int selectedProductId = -1;

        public InventoryForm()
        {
            InitializeComponent();
            _inventoryService = new InventoryService();
            _supplierService = new SupplierService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(InventoryForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlForm.BackColor = ThemeHelper.SecondaryDark;
            pnlGrid.BackColor = ThemeHelper.PrimaryDark;
            btnSave.BackColor = ThemeHelper.AccentBlue;
            btnDelete.BackColor = ThemeHelper.AccentRed;
            dgvProducts.CellFormatting += DgvProducts_CellFormatting;
            txtSearch.TextChanged += (s, e) => LoadProducts(txtSearch.Text.Trim());
        }

        private void DgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProducts.Columns[e.ColumnIndex].Name == "StockQuantity")
            {
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int stock))
                {
                    int minStock = Convert.ToInt32(dgvProducts.Rows[e.RowIndex].Cells["MinStockLevel"].Value);

                    if (stock <= 0)
                        dgvProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = ThemeHelper.LevelCritical;
                    else if (stock <= minStock)
                        dgvProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = ThemeHelper.LevelWarning;
                    else
                        dgvProducts.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                }
            }
        }

        private void InventoryForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtProductName.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDelete.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnClear.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                txtSearch.Focus();
                e.Handled = true;
            }
        }

        private void InventoryForm_Load(object? sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();
            LoadTaxCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategory.DataSource = _inventoryService.GetCategories();
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void LoadTaxCategories()
        {
            try
            {
                cmbTaxCategory.DataSource = _inventoryService.GetTaxCategories();
                cmbTaxCategory.DisplayMember = "TaxName";
                cmbTaxCategory.ValueMember = "TaxCategoryID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tax categories: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                cmbSupplier.DataSource = _supplierService.GetAllSuppliers();
                cmbSupplier.DisplayMember = "SupplierName";
                cmbSupplier.ValueMember = "SupplierID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
        }

        private void LoadProducts(string search = "")
        {
            try
            {
                var dt = _inventoryService.GetAllProducts();
                if (!string.IsNullOrEmpty(search))
                {
                    dt.DefaultView.RowFilter = $"ProductName LIKE '%{search}%' OR SKU LIKE '%{search}%' OR Barcode LIKE '%{search}%'";
                    dgvProducts.DataSource = dt.DefaultView;
                }
                else
                {
                    dgvProducts.DataSource = dt;
                }

                string[] hideCols = { "CategoryID", "SupplierID", "TaxCategoryID" };
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
                Product product = new Product
                {
                    ProductID = selectedProductId,
                    ProductName = txtProductName.Text.Trim(),
                    SKU = txtSKU.Text.Trim(),
                    Barcode = txtBarcode.Text.Trim(),
                    CategoryID = cmbCategory.SelectedValue != null ? Convert.ToInt32(cmbCategory.SelectedValue) : (int?)null,
                    SupplierID = cmbSupplier.SelectedValue != null ? Convert.ToInt32(cmbSupplier.SelectedValue) : (int?)null,
                    TaxCategoryID = cmbTaxCategory.SelectedValue != null ? Convert.ToInt32(cmbTaxCategory.SelectedValue) : 3, // Default to Zero Rated
                    Brand = txtBrand.Text.Trim(),
                    Price = price,
                    StockQuantity = quantity,
                    MinStockLevel = int.TryParse(txtMinStock.Text, out int min) ? min : 10,
                    DiscountRate = decimal.TryParse(txtDiscountRate.Text, out decimal disc) ? disc : 0,
                    IsBOGO = chkIsBOGO.Checked,
                    UnitType = cmbUnitType.SelectedItem?.ToString()
                };

                _inventoryService.SaveProduct(product);
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
                    _inventoryService.DeleteProduct(selectedProductId);
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
            txtMinStock.Text = "10";
            txtDiscountRate.Text = "0";
            chkIsBOGO.Checked = false;
            cmbUnitType.SelectedIndex = 0;
            if (cmbTaxCategory.Items.Count > 0) cmbTaxCategory.SelectedIndex = 0;
            selectedProductId = -1;
            btnSave.Text = "Save (F2)";
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
                txtMinStock.Text = row.Cells["MinStockLevel"].Value?.ToString();
                txtDiscountRate.Text = row.Cells["DiscountRate"].Value?.ToString();
                chkIsBOGO.Checked = row.Cells["IsBOGO"].Value != DBNull.Value && Convert.ToBoolean(row.Cells["IsBOGO"].Value);

                if (row.Cells["CategoryID"].Value != DBNull.Value)
                    cmbCategory.SelectedValue = row.Cells["CategoryID"].Value;

                if (row.Cells["SupplierID"].Value != DBNull.Value)
                    cmbSupplier.SelectedValue = row.Cells["SupplierID"].Value;

                if (row.Cells["TaxCategoryID"].Value != DBNull.Value)
                    cmbTaxCategory.SelectedValue = row.Cells["TaxCategoryID"].Value;

                if (row.Cells["UnitType"].Value != DBNull.Value)
                    cmbUnitType.SelectedItem = row.Cells["UnitType"].Value.ToString();

                btnSave.Text = "Update (F2)";
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (var form = new Form())
            {
                form.Text = "Add Category";
                form.Size = new Size(300, 150);
                form.StartPosition = FormStartPosition.CenterParent;
                Label lbl = new Label() { Text = "Category Name:", Left = 20, Top = 20, Width = 100 };
                TextBox txt = new TextBox() { Left = 20, Top = 45, Width = 240 };
                Button btn = new Button() { Text = "Add", Left = 180, Top = 80, Width = 80 };
                btn.Click += (s, ev) => {
                    if (!string.IsNullOrWhiteSpace(txt.Text))
                    {
                        try {
                            var db = new Data.DatabaseHelper();
                            db.ExecuteNonQuery("INSERT INTO Categories (CategoryName) VALUES (@name)",
                                new MySql.Data.MySqlClient.MySqlParameter[] { new MySql.Data.MySqlClient.MySqlParameter("@name", txt.Text.Trim()) });
                            form.DialogResult = DialogResult.OK;
                        } catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                };
                form.Controls.Add(lbl); form.Controls.Add(txt); form.Controls.Add(btn);
                if (form.ShowDialog() == DialogResult.OK) LoadCategories();
            }
        }
    }
}
