using System;
using System.Data;
using System.Windows.Forms;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class ExpenseForm : Form
    {
        private readonly ExpenseService _expenseService;

        public ExpenseForm()
        {
            InitializeComponent();
            _expenseService = new ExpenseService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(ExpenseForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnSave.BackColor = ThemeHelper.AccentBlue;
        }

        private void ExpenseForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtTitle.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSave.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                txtTitle.Clear();
                txtAmount.Clear();
                txtDescription.Clear();
                e.Handled = true;
            }
        }

        private void ExpenseForm_Load(object sender, EventArgs e)
        {
            LoadExpenses();
            txtSearch.TextChanged += (s, ev) => LoadExpenses(txtSearch.Text);
        }

        private void LoadExpenses(string filter = "")
        {
            try
            {
                var data = _expenseService.GetAllExpenses();
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    data.DefaultView.RowFilter = string.Format("title LIKE '%{0}%' OR category LIKE '%{0}%'", filter.Replace("'", "''"));
                }
                dgvExpenses.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading expenses: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid title and amount.");
                return;
            }

            try
            {
                string category = "Other";
                if (cmbCategory.SelectedIndex != -1)
                {
                    category = cmbCategory.SelectedItem?.ToString() ?? "Other";
                }

                _expenseService.AddExpense(
                    txtTitle.Text.Trim(),
                    category,
                    amount,
                    dtpDate.Value,
                    txtDescription.Text.Trim()
                );

                MessageBox.Show("Expense saved successfully.");
                txtTitle.Clear();
                txtAmount.Clear();
                txtDescription.Clear();
                LoadExpenses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving expense: " + ex.Message);
            }
        }
    }
}
