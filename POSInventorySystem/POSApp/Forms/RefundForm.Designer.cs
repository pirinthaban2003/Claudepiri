using System.ComponentModel;

namespace POSApp.Forms
{
    partial class RefundForm
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtSaleID = new System.Windows.Forms.TextBox();
            this.btnSearchSale = new System.Windows.Forms.Button();
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnProcessRefund = new System.Windows.Forms.Button();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            this.SuspendLayout();
            //
            // txtSaleID
            //
            this.txtSaleID.Location = new System.Drawing.Point(80, 20);
            this.txtSaleID.Name = "txtSaleID";
            this.txtSaleID.Size = new System.Drawing.Size(100, 23);
            this.txtSaleID.TabIndex = 0;
            //
            // btnSearchSale
            //
            this.btnSearchSale.Location = new System.Drawing.Point(190, 19);
            this.btnSearchSale.Name = "btnSearchSale";
            this.btnSearchSale.Size = new System.Drawing.Size(100, 25);
            this.btnSearchSale.TabIndex = 1;
            this.btnSearchSale.Text = "Search Sale";
            this.btnSearchSale.UseVisualStyleBackColor = true;
            this.btnSearchSale.Click += new System.EventHandler(this.btnSearchSale_Click);
            //
            // dgvSaleItems
            //
            this.dgvSaleItems.AllowUserToAddRows = false;
            this.dgvSaleItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleItems.Location = new System.Drawing.Point(20, 80);
            this.dgvSaleItems.Name = "dgvSaleItems";
            this.dgvSaleItems.Size = new System.Drawing.Size(760, 200);
            this.dgvSaleItems.TabIndex = 2;
            //
            // txtReason
            //
            this.txtReason.Location = new System.Drawing.Point(20, 310);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(400, 60);
            this.txtReason.TabIndex = 3;
            //
            // btnProcessRefund
            //
            this.btnProcessRefund.BackColor = System.Drawing.Color.Tomato;
            this.btnProcessRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessRefund.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnProcessRefund.ForeColor = System.Drawing.Color.White;
            this.btnProcessRefund.Location = new System.Drawing.Point(20, 380);
            this.btnProcessRefund.Name = "btnProcessRefund";
            this.btnProcessRefund.Size = new System.Drawing.Size(200, 50);
            this.btnProcessRefund.TabIndex = 4;
            this.btnProcessRefund.Text = "Process Refund";
            this.btnProcessRefund.UseVisualStyleBackColor = false;
            this.btnProcessRefund.Click += new System.EventHandler(this.btnProcessRefund_Click);
            //
            // lblSaleID
            //
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.Location = new System.Drawing.Point(20, 23);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(45, 15);
            this.lblSaleID.TabIndex = 5;
            this.lblSaleID.Text = "Sale ID:";
            //
            // lblReason
            //
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(20, 290);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(89, 15);
            this.lblReason.TabIndex = 6;
            this.lblReason.Text = "Refund Reason:";
            //
            // lblItems
            //
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblItems.Location = new System.Drawing.Point(20, 60);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(130, 15);
            this.lblItems.TabIndex = 7;
            this.lblItems.Text = "Items in Sale (Check to return):";
            //
            // RefundForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.lblSaleID);
            this.Controls.Add(this.btnProcessRefund);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.dgvSaleItems);
            this.Controls.Add(this.btnSearchSale);
            this.Controls.Add(this.txtSaleID);
            this.Name = "RefundForm";
            this.Text = "Sales Returns & Refunds";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtSaleID;
        private System.Windows.Forms.Button btnSearchSale;
        private System.Windows.Forms.DataGridView dgvSaleItems;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnProcessRefund;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblItems;
    }
}
