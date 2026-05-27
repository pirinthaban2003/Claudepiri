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
                    object? minStockVal = GetCellValue(dgvProducts.Rows[e.RowIndex], "MinStockLevel");
                    int minStock = minStockVal != null && minStockVal != DBNull.Value ? Convert.ToInt32(minStockVal) : 10;

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
                    string safeSearch = search.Replace("'", "''");
                    dt.DefaultView.RowFilter = $"ProductName LIKE '%{safeSearch}%' OR SKU LIKE '%{safeSearch}%' OR Barcode LIKE '%{safeSearch}%'";
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
            if (cmbUnitType.Items.Count > 0) cmbUnitType.SelectedIndex = 0;
            if (cmbTaxCategory.Items.Count > 0) cmbTaxCategory.SelectedIndex = 0;
            selectedProductId = -1;
            btnSave.Text = "Save (F2)";
        }

        private object? GetCellValue(DataGridViewRow row, string columnName)
        {
            if (row.DataGridView?.Columns.Contains(columnName) == true)
            {
                return row.Cells[columnName].Value;
            }
            return null;
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                var prodIdVal = GetCellValue(row, "ProductID");
                if (prodIdVal != null && prodIdVal != DBNull.Value)
                    selectedProductId = Convert.ToInt32(prodIdVal);

                txtProductName.Text = GetCellValue(row, "ProductName")?.ToString();
                txtSKU.Text = GetCellValue(row, "SKU")?.ToString();
                txtBarcode.Text = GetCellValue(row, "Barcode")?.ToString();
                txtBrand.Text = GetCellValue(row, "Brand")?.ToString();
                txtPrice.Text = GetCellValue(row, "Price")?.ToString();
                txtQuantity.Text = GetCellValue(row, "StockQuantity")?.ToString();
                txtMinStock.Text = GetCellValue(row, "MinStockLevel")?.ToString();
                txtDiscountRate.Text = GetCellValue(row, "DiscountRate")?.ToString() ?? "0";

                var bogoVal = GetCellValue(row, "IsBOGO");
                chkIsBOGO.Checked = bogoVal != null && bogoVal != DBNull.Value && Convert.ToBoolean(bogoVal);

                var catIdVal = GetCellValue(row, "CategoryID");
                if (catIdVal != null && catIdVal != DBNull.Value)
                    cmbCategory.SelectedValue = catIdVal;

                var supIdVal = GetCellValue(row, "SupplierID");
                if (supIdVal != null && supIdVal != DBNull.Value)
                    cmbSupplier.SelectedValue = supIdVal;

                var taxIdVal = GetCellValue(row, "TaxCategoryID");
                if (taxIdVal != null && taxIdVal != DBNull.Value)
                    cmbTaxCategory.SelectedValue = taxIdVal;

                var unitVal = GetCellValue(row, "UnitType");
                if (unitVal != null && unitVal != DBNull.Value)
                    cmbUnitType.SelectedItem = unitVal.ToString();

                btnSave.Text = "Update (F2)";
            }
        }

        private void btnManageBatches_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            using (var form = new BatchManagementForm(selectedProductId, txtProductName.Text))
            {
                form.ShowDialog();
                LoadProducts(); // Refresh stock
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
