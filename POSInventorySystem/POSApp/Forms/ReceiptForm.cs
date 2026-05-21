using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Utilities;
using MySql.Data.MySqlClient;
using POSApp.Data;

namespace POSApp.Forms
{
    public partial class ReceiptForm : Form
    {
        private readonly Sale _sale;
        private readonly DatabaseHelper _dbHelper;

        public ReceiptForm(Sale sale)
        {
            InitializeComponent();
            _sale = sale;
            _dbHelper = new DatabaseHelper();
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
            GenerateReceiptText();
        }

        private void CustomizeComponents()
        {
            lblPreview.ForeColor = ThemeHelper.AccentBlue;
            txtReceipt.BackColor = System.Drawing.Color.White;
            txtReceipt.ForeColor = System.Drawing.Color.Black;
        }

        private void GenerateReceiptText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("******************************************");
            sb.AppendLine("      SUPERMARKET RETAIL SYSTEM           ");
            sb.AppendLine("            SRMS TERMINAL                 ");
            sb.AppendLine("******************************************");
            sb.AppendLine($"Date: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Cashier: {Session.CurrentUser?.FullName ?? "Staff"}");

            // Customer Info
            if (_sale.CustomerID.HasValue)
            {
                var dt = _dbHelper.ExecuteQuery("SELECT CustomerName, LoyaltyPoints, LoyaltyLevel FROM Customers WHERE CustomerID = @id",
                    new MySqlParameter[] { new MySqlParameter("@id", _sale.CustomerID.Value) });
                if (dt.Rows.Count > 0)
                {
                    sb.AppendLine($"Customer: {dt.Rows[0]["CustomerName"]}");
                    sb.AppendLine($"Loyalty Level: {dt.Rows[0]["LoyaltyLevel"]}");
                }
            }

            sb.AppendLine("------------------------------------------");
            sb.AppendLine(string.Format("{0,-20} {1,5} {2,10}", "Item", "Qty", "Price"));
            sb.AppendLine("------------------------------------------");

            foreach (var item in _sale.Items)
            {
                string name = item.ProductName.Length > 18 ? item.ProductName.Substring(0, 18) : item.ProductName;
                sb.AppendLine(string.Format("{0,-20} {1,5} {2,10:N2}", name, item.Quantity, item.Subtotal));
            }

            sb.AppendLine("------------------------------------------");
            sb.AppendLine(string.Format("{0,-26} {1,14:N2}", "Subtotal:", _sale.TotalAmount));
            sb.AppendLine(string.Format("{0,-26} {1,14:N2}", "Discount:", _sale.DiscountAmount));
            sb.AppendLine(string.Format("{0,-26} {1,14:N2}", "Tax:", _sale.TaxAmount));
            sb.AppendLine("==========================================");
            sb.AppendLine(string.Format("TOTAL: {0,30:C}", _sale.FinalAmount));

            // Loyalty Points Info
            if (_sale.CustomerID.HasValue)
            {
                int pointsEarned = (int)(_sale.FinalAmount / 100);
                sb.AppendLine("==========================================");
                sb.AppendLine($"Points Earned this visit: {pointsEarned}");
            }

            sb.AppendLine("==========================================");
            sb.AppendLine("\n      THANK YOU FOR SHOPPING!             ");
            sb.AppendLine("       Please visit us again              ");
            sb.AppendLine("******************************************");

            txtReceipt.Text = sb.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
