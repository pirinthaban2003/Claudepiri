using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class BatchManagementForm : Form
    {
        private readonly InventoryService _inventoryService;
        private readonly int _productId;
        private readonly string _productName;

        public BatchManagementForm(int productId, string productName)
        {
            InitializeComponent();
            _productId = productId;
            _productName = productName;
            _inventoryService = new InventoryService();
            ThemeHelper.ApplyTheme(this);
            lblProduct.Text = "Managing Batches for: " + _productName;
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            btnAddBatch.BackColor = ThemeHelper.AccentGreen;
            dgvBatches.DataSource = _inventoryService.GetProductBatches(_productId);
        }

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddBatch_Click(object? sender, EventArgs e)
        {
            if (!decimal.TryParse(txtCostPrice.Text, out decimal cost))
            {
                MessageBox.Show("Invalid cost price.");
                return;
            }

            if (!decimal.TryParse(txtSellingPrice.Text, out decimal selling))
            {
                MessageBox.Show("Invalid selling price.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty))
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            try
            {
                InventoryBatch batch = new InventoryBatch
                {
                    ProductID = _productId,
                    BatchNumber = txtBatchNumber.Text.Trim(),
                    CostPrice = cost,
                    SellingPrice = selling,
                    Quantity = qty,
                    ExpiryDate = dtpExpiry.Value
                };

                _inventoryService.AddBatch(batch);
                MessageBox.Show("Batch added and stock updated!");
                txtBatchNumber.Clear();
                txtCostPrice.Clear();
                txtSellingPrice.Clear();
                txtQuantity.Clear();
                dgvBatches.DataSource = _inventoryService.GetProductBatches(_productId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
