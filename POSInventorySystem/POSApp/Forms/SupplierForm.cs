using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Forms
{
    public partial class SupplierForm : Form
    {
        private DatabaseHelper dbHelper;
        private int selectedSupplierId = -1;

        public SupplierForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            try
            {
                dgvSuppliers.DataSource = dbHelper.GetAllSuppliers();
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

                if (selectedSupplierId == -1)
                {
                    dbHelper.AddSupplier(supplier);
                }
                else
                {
                    dbHelper.UpdateSupplier(supplier);
                }

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
                    dbHelper.DeleteSupplier(selectedSupplierId);
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
            btnSave.Text = "Save";
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
                btnSave.Text = "Update";
            }
        }
    }
}
