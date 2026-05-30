using System.Drawing;
using System.Windows.Forms;

namespace POSApp.Forms
{
    partial class BatchManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblProduct = new System.Windows.Forms.Label();
            this.dgvBatches = new System.Windows.Forms.DataGridView();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.btnAddBatch = new System.Windows.Forms.Button();
            this.lblBatchNum = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblSelling = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).BeginInit();
            this.SuspendLayout();
            //
            // lblProduct
            //
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProduct.Location = new System.Drawing.Point(50, 10);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(200, 21);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Managing Batches";
            //
            // dgvBatches
            //
            this.dgvBatches.AllowUserToAddRows = false;
            this.dgvBatches.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBatches.Location = new System.Drawing.Point(20, 60);
            this.dgvBatches.Name = "dgvBatches";
            this.dgvBatches.ReadOnly = true;
            this.dgvBatches.Size = new System.Drawing.Size(540, 200);
            this.dgvBatches.TabIndex = 1;
            //
            // txtBatchNumber
            //
            this.txtBatchNumber.Location = new System.Drawing.Point(120, 280);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(150, 23);
            this.txtBatchNumber.TabIndex = 2;
            //
            // txtCostPrice
            //
            this.txtCostPrice.Location = new System.Drawing.Point(120, 315);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(150, 23);
            this.txtCostPrice.TabIndex = 3;
            //
            // txtSellingPrice
            //
            this.txtSellingPrice.Location = new System.Drawing.Point(120, 350);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(150, 23);
            this.txtSellingPrice.TabIndex = 4;
            //
            // txtQuantity
            //
            this.txtQuantity.Location = new System.Drawing.Point(120, 385);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(150, 23);
            this.txtQuantity.TabIndex = 5;
            //
            // dtpExpiry
            //
            this.dtpExpiry.Location = new System.Drawing.Point(120, 420);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(150, 23);
            this.dtpExpiry.TabIndex = 6;
            //
            // btnAddBatch
            //
            this.btnAddBatch.Location = new System.Drawing.Point(120, 455);
            this.btnAddBatch.Name = "btnAddBatch";
            this.btnAddBatch.Size = new System.Drawing.Size(150, 40);
            this.btnAddBatch.TabIndex = 7;
            this.btnAddBatch.Text = "ADD BATCH";
            this.btnAddBatch.UseVisualStyleBackColor = true;
            this.btnAddBatch.Click += new System.EventHandler(this.btnAddBatch_Click);
            //
            // lblBatchNum
            //
            this.lblBatchNum.AutoSize = true;
            this.lblBatchNum.Location = new System.Drawing.Point(20, 283);
            this.lblBatchNum.Name = "lblBatchNum";
            this.lblBatchNum.Size = new System.Drawing.Size(51, 15);
            this.lblBatchNum.TabIndex = 8;
            this.lblBatchNum.Text = "Batch #:";
            //
            // lblCost
            //
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(20, 318);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(63, 15);
            this.lblCost.TabIndex = 9;
            this.lblCost.Text = "Cost Price:";
            //
            // lblSelling
            //
            this.lblSelling.AutoSize = true;
            this.lblSelling.Location = new System.Drawing.Point(20, 353);
            this.lblSelling.Name = "lblSelling";
            this.lblSelling.Size = new System.Drawing.Size(74, 15);
            this.lblSelling.TabIndex = 10;
            this.lblSelling.Text = "Selling Price:";
            //
            // lblQty
            //
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(20, 388);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(56, 15);
            this.lblQty.TabIndex = 11;
            this.lblQty.Text = "Quantity:";
            //
            // lblExpiry
            //
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(20, 423);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(42, 15);
            this.lblExpiry.TabIndex = 12;
            this.lblExpiry.Text = "Expiry:";
            //
            // btnBack
            //
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 25);
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // BatchManagementForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblExpiry);
            this.Controls.Add(this.dtpExpiry);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lblSelling);
            this.Controls.Add(this.txtSellingPrice);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.txtCostPrice);
            this.Controls.Add(this.lblBatchNum);
            this.Controls.Add(this.btnAddBatch);
            this.Controls.Add(this.txtBatchNumber);
            this.Controls.Add(this.dgvBatches);
            this.Controls.Add(this.lblProduct);
            this.Name = "BatchManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBatches)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.DataGridView dgvBatches;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Button btnAddBatch;
        private System.Windows.Forms.Label lblBatchNum;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblSelling;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Button btnBack;
    }
}
