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

    // Manual Designer code for simplicity in this agent environment
    partial class BatchManagementForm
    {
        private System.Windows.Forms.Label lblProduct = new System.Windows.Forms.Label();
        private System.Windows.Forms.DataGridView dgvBatches = new System.Windows.Forms.DataGridView();
        private System.Windows.Forms.TextBox txtBatchNumber = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox txtCostPrice = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox txtSellingPrice = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox txtQuantity = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.DateTimePicker dtpExpiry = new System.Windows.Forms.DateTimePicker();
        private System.Windows.Forms.Button btnAddBatch = new System.Windows.Forms.Button();
        private System.Windows.Forms.Label lblBatchNum = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblCost = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblSelling = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblQty = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblExpiry = new System.Windows.Forms.Label();
        private System.Windows.Forms.Button btnBack = new System.Windows.Forms.Button();

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).BeginInit();
            this.SuspendLayout();

            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProduct.Location = new System.Drawing.Point(50, 10);
            this.lblProduct.Size = new System.Drawing.Size(400, 21);

            this.dgvBatches.Location = new System.Drawing.Point(20, 60);
            this.dgvBatches.Size = new System.Drawing.Size(540, 200);
            this.dgvBatches.ReadOnly = true;

            int left = 20, top = 280, labelWidth = 100, inputWidth = 150, spacing = 35;

            this.lblBatchNum.Text = "Batch #:";
            this.lblBatchNum.Location = new Point(left, top);
            this.txtBatchNumber.Location = new Point(left + labelWidth, top);
            this.txtBatchNumber.Width = inputWidth;

            this.lblCost.Text = "Cost Price:";
            this.lblCost.Location = new Point(left, top + spacing);
            this.txtCostPrice.Location = new Point(left + labelWidth, top + spacing);
            this.txtCostPrice.Width = inputWidth;

            this.lblSelling.Text = "Selling Price:";
            this.lblSelling.Location = new Point(left, top + spacing * 2);
            this.txtSellingPrice.Location = new Point(left + labelWidth, top + spacing * 2);
            this.txtSellingPrice.Width = inputWidth;

            this.lblQty.Text = "Quantity:";
            this.lblQty.Location = new Point(left, top + spacing * 3);
            this.txtQuantity.Location = new Point(left + labelWidth, top + spacing * 3);
            this.txtQuantity.Width = inputWidth;

            this.lblExpiry.Text = "Expiry:";
            this.lblExpiry.Location = new Point(left, top + spacing * 4);
            this.dtpExpiry.Location = new Point(left + labelWidth, top + spacing * 4);
            this.dtpExpiry.Width = inputWidth;

            this.btnBack.Text = "←";
            this.btnBack.Location = new Point(5, 5);
            this.btnBack.Size = new Size(40, 25);
            this.btnBack.Click += new EventHandler(btnBack_Click);
            this.Controls.Add(btnBack);

            this.btnAddBatch.Text = "ADD BATCH";
            this.btnAddBatch.Location = new Point(left + labelWidth, top + spacing * 5);
            this.btnAddBatch.Size = new Size(inputWidth, 40);
            this.btnAddBatch.Click += new EventHandler(btnAddBatch_Click);

            this.ClientSize = new System.Drawing.Size(600, 500);
            this.Controls.Add(lblProduct);
            this.Controls.Add(dgvBatches);
            this.Controls.Add(lblBatchNum); this.Controls.Add(txtBatchNumber);
            this.Controls.Add(lblCost); this.Controls.Add(txtCostPrice);
            this.Controls.Add(lblSelling); this.Controls.Add(txtSellingPrice);
            this.Controls.Add(lblQty); this.Controls.Add(txtQuantity);
            this.Controls.Add(lblExpiry); this.Controls.Add(dtpExpiry);
            this.Controls.Add(btnAddBatch);
            this.Text = "Batch Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
