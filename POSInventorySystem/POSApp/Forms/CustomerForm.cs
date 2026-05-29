using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private int selectedCustomerId = -1;

        public CustomerForm()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(CustomerForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlForm.BackColor = ThemeHelper.SecondaryDark;
            pnlGrid.BackColor = ThemeHelper.PrimaryDark;
            btnSave.BackColor = ThemeHelper.AccentBlue;
            btnDelete.BackColor = ThemeHelper.AccentRed;
            btnViewHistory.BackColor = ThemeHelper.AccentBlue;
            btnViewHistory.Enabled = false;
            txtSearch.TextChanged += (s, e) => LoadCustomers(txtSearch.Text.Trim());
        }

        private void CustomerForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtCustomerName.Focus();
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

        private void CustomerForm_Load(object? sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers(string search = "")
        {
            try
            {
                var dt = _customerService.GetAllCustomers();
                if (!string.IsNullOrEmpty(search))
                {
                    string safeSearch = search.Replace("'", "''");
                    dt.DefaultView.RowFilter = $"CustomerName LIKE '%{safeSearch}%' OR Phone LIKE '%{safeSearch}%' OR Email LIKE '%{safeSearch}%'";
                    dgvCustomers.DataSource = dt.DefaultView;
                }
                else
                {
                    dgvCustomers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer Name is required.");
                return;
            }

            int points = 0;
            decimal wallet = 0;
            int.TryParse(txtPoints.Text, out points);
            decimal.TryParse(txtWallet.Text, out wallet);

            try
            {
                Customer customer = new Customer
                {
                    CustomerID = selectedCustomerId,
                    CustomerName = txtCustomerName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    LoyaltyPoints = points,
                    LoyaltyLevel = txtLevel.Text.Trim(),
                    WalletBalance = wallet
                };

                _customerService.SaveCustomer(customer);
                ClearFields();
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving customer: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == -1) return;

            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _customerService.DeleteCustomer(selectedCustomerId);
                    ClearFields();
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting customer: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtPoints.Clear();
            txtWallet.Clear();
            txtLevel.Clear();
            selectedCustomerId = -1;
            btnSave.Text = "Save (F2)";
            btnViewHistory.Enabled = false;
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                selectedCustomerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                txtCustomerName.Text = row.Cells["CustomerName"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();
                txtPoints.Text = row.Cells["LoyaltyPoints"].Value?.ToString();
                txtWallet.Text = row.Cells["WalletBalance"].Value?.ToString();
                txtLevel.Text = row.Cells["LoyaltyLevel"].Value?.ToString();
                btnSave.Text = "Update (F2)";
                btnViewHistory.Enabled = true;
            }
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId != -1)
            {
                SalesHistoryForm historyForm = new SalesHistoryForm(selectedCustomerId);
                historyForm.ShowDialog();
            }
        }
    }
}
