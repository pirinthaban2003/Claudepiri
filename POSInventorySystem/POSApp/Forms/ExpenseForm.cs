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
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnSave.BackColor = ThemeHelper.AccentBlue;
        }

        private void ExpenseForm_Load(object sender, EventArgs e)
        {
            LoadExpenses();
        }

        private void LoadExpenses()
        {
            try
            {
                dgvExpenses.DataSource = _expenseService.GetAllExpenses();
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
                _expenseService.AddExpense(
                    txtTitle.Text.Trim(),
                    cmbCategory.SelectedItem?.ToString() ?? "Other",
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
