using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class SupplierForm : Form
    {
        private readonly SupplierService _supplierService;
        private int selectedSupplierId = -1;

        public SupplierForm()
        {
            InitializeComponent();
            _supplierService = new SupplierService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(SupplierForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlForm.BackColor = ThemeHelper.SecondaryDark;
            pnlGrid.BackColor = ThemeHelper.PrimaryDark;
            btnSave.BackColor = ThemeHelper.AccentBlue;
            btnDelete.BackColor = ThemeHelper.AccentRed;
            txtSearch.TextChanged += (s, e) => LoadSuppliers(txtSearch.Text.Trim());
        }

        private void SupplierForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtSupplierName.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
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

        private void SupplierForm_Load(object? sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void LoadSuppliers(string search = "")
        {
            try
            {
                var dt = _supplierService.GetAllSuppliers();
                if (!string.IsNullOrEmpty(search))
                {
                    string safeSearch = search.Replace("'", "''");
                    dt.DefaultView.RowFilter = $"SupplierName LIKE '%{safeSearch}%' OR ContactPerson LIKE '%{safeSearch}%' OR Phone LIKE '%{safeSearch}%'";
                    dgvSuppliers.DataSource = dt.DefaultView;
                }
                else
                {
                    dgvSuppliers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Supplier Name is required.");
                return;
            }

            try
            {
                Supplier supplier = new Supplier
                {
                    SupplierID = selectedSupplierId,
                    SupplierName = txtSupplierName.Text.Trim(),
                    ContactPerson = txtContactPerson.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                _supplierService.SaveSupplier(supplier);
                ClearFields();
                LoadSuppliers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving supplier: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId == -1) return;

            if (MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _supplierService.DeleteSupplier(selectedSupplierId);
                    ClearFields();
                    LoadSuppliers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting supplier: " + ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtSupplierName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            selectedSupplierId = -1;
            btnSave.Text = "Save (F2)";
        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
                selectedSupplierId = Convert.ToInt32(row.Cells["SupplierID"].Value);
                txtSupplierName.Text = row.Cells["SupplierName"].Value?.ToString();
                txtContactPerson.Text = row.Cells["ContactPerson"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                btnSave.Text = "Update (F2)";
            }
        }
    }
}
