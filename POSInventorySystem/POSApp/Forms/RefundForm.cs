using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class RefundForm : Form
    {
        private readonly ReturnService _returnService;

        public RefundForm()
        {
            InitializeComponent();
            _returnService = new ReturnService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(RefundForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            SetupGrid();
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnProcessRefund.BackColor = ThemeHelper.AccentRed;
        }

        private void RefundForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtSaleID.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnProcessRefund.PerformClick();
                e.Handled = true;
            }
        }

        private void SetupGrid()
        {
            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "Return?";
            checkColumn.Width = 50;
            dgvSaleItems.Columns.Insert(0, checkColumn);
        }

        private void btnSearchSale_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSaleID.Text, out int saleId))
            {
                try
                {
                    DataTable dt = _returnService.GetSaleDetails(saleId);
                    if (dt.Rows.Count > 0)
                    {
                        dgvSaleItems.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Sale not found or has no items.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching sale: " + ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProcessRefund_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSaleID.Text, out int saleId)) return;

            List<ReturnItem> itemsToReturn = new List<ReturnItem>();
            foreach (DataGridViewRow row in dgvSaleItems.Rows)
            {
                bool isSelected = Convert.ToBoolean(row.Cells["Select"].Value);
                if (isSelected)
                {
                    itemsToReturn.Add(new ReturnItem
                    {
                        ProductID = Convert.ToInt32(row.Cells["ProductID"].Value),
                        Quantity = Convert.ToInt32(row.Cells["Quantity"].Value),
                        RefundAmount = Convert.ToDecimal(row.Cells["Subtotal"].Value)
                    });
                }
            }

            if (itemsToReturn.Count == 0)
            {
                MessageBox.Show("Please select at least one item to return.");
                return;
            }

            try
            {
                if (_returnService.ProcessReturn(saleId, itemsToReturn, txtReason.Text))
                {
                    MessageBox.Show("Refund processed successfully! Inventory has been updated.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing refund: " + ex.Message);
            }
        }
    }
}
