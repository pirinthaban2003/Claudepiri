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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblSaleID = new System.Windows.Forms.Label();
            this.txtSaleID = new System.Windows.Forms.TextBox();
            this.btnSearchSale = new System.Windows.Forms.Button();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnProcessRefund = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblItems = new System.Windows.Forms.Label();
            this.dgvSaleItems = new System.Windows.Forms.DataGridView();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).BeginInit();
            this.SuspendLayout();
            //
            // pnlLeft
            //
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlLeft.Controls.Add(this.btnBack);
            this.pnlLeft.Controls.Add(this.lblFormTitle);
            this.pnlLeft.Controls.Add(this.lblSaleID);
            this.pnlLeft.Controls.Add(this.txtSaleID);
            this.pnlLeft.Controls.Add(this.btnSearchSale);
            this.pnlLeft.Controls.Add(this.lblReason);
            this.pnlLeft.Controls.Add(this.txtReason);
            this.pnlLeft.Controls.Add(this.btnProcessRefund);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20);
            this.pnlLeft.Size = new System.Drawing.Size(300, 600);
            this.pnlLeft.TabIndex = 0;
            //
            // btnBack
            //
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 25);
            this.btnBack.TabIndex = 22;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // lblFormTitle
            //
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.Location = new System.Drawing.Point(50, 10);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(149, 25);
            this.lblFormTitle.TabIndex = 7;
            this.lblFormTitle.Text = "Process Refund";
            //
            // lblSaleID
            //
            this.lblSaleID.AutoSize = true;
            this.lblSaleID.ForeColor = System.Drawing.Color.White;
            this.lblSaleID.Location = new System.Drawing.Point(20, 45);
            this.lblSaleID.Name = "lblSaleID";
            this.lblSaleID.Size = new System.Drawing.Size(45, 15);
            this.lblSaleID.TabIndex = 5;
            this.lblSaleID.Text = "Sale ID:";
            //
            // txtSaleID
            //
            this.txtSaleID.Location = new System.Drawing.Point(120, 40);
            this.txtSaleID.Name = "txtSaleID";
            this.txtSaleID.Size = new System.Drawing.Size(160, 23);
            this.txtSaleID.TabIndex = 0;
            //
            // btnSearchSale
            //
            this.btnSearchSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSearchSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchSale.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearchSale.ForeColor = System.Drawing.Color.White;
            this.btnSearchSale.Location = new System.Drawing.Point(20, 80);
            this.btnSearchSale.Name = "btnSearchSale";
            this.btnSearchSale.Size = new System.Drawing.Size(260, 35);
            this.btnSearchSale.TabIndex = 1;
            this.btnSearchSale.Text = "SEARCH SALE";
            this.btnSearchSale.UseVisualStyleBackColor = false;
            this.btnSearchSale.Click += new System.EventHandler(this.btnSearchSale_Click);
            //
            // lblReason
            //
            this.lblReason.AutoSize = true;
            this.lblReason.ForeColor = System.Drawing.Color.White;
            this.lblReason.Location = new System.Drawing.Point(20, 130);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(89, 15);
            this.lblReason.TabIndex = 6;
            this.lblReason.Text = "Refund Reason:";
            //
            // txtReason
            //
            this.txtReason.Location = new System.Drawing.Point(20, 150);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(260, 100);
            this.txtReason.TabIndex = 3;
            //
            // btnProcessRefund
            //
            this.btnProcessRefund.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.btnProcessRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessRefund.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnProcessRefund.ForeColor = System.Drawing.Color.White;
            this.btnProcessRefund.Location = new System.Drawing.Point(20, 270);
            this.btnProcessRefund.Name = "btnProcessRefund";
            this.btnProcessRefund.Size = new System.Drawing.Size(260, 50);
            this.btnProcessRefund.TabIndex = 4;
            this.btnProcessRefund.Text = "PROCESS REFUND";
            this.btnProcessRefund.UseVisualStyleBackColor = false;
            this.btnProcessRefund.Click += new System.EventHandler(this.btnProcessRefund_Click);
            //
            // pnlRight
            //
            this.pnlRight.Controls.Add(this.lblItems);
            this.pnlRight.Controls.Add(this.dgvSaleItems);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(300, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(20);
            this.pnlRight.Size = new System.Drawing.Size(700, 600);
            this.pnlRight.TabIndex = 1;
            //
            // lblItems
            //
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblItems.Location = new System.Drawing.Point(20, 20);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(209, 19);
            this.lblItems.TabIndex = 7;
            this.lblItems.Text = "Items in Sale (Check to return):";
            //
            // dgvSaleItems
            //
            this.dgvSaleItems.AllowUserToAddRows = false;
            this.dgvSaleItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSaleItems.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgvSaleItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaleItems.Location = new System.Drawing.Point(20, 50);
            this.dgvSaleItems.Name = "dgvSaleItems";
            this.dgvSaleItems.Size = new System.Drawing.Size(660, 530);
            this.dgvSaleItems.TabIndex = 2;
            //
            // RefundForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Name = "RefundForm";
            this.Text = "Sales Returns & Refunds";
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaleItems)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.TextBox txtSaleID;
        private System.Windows.Forms.Button btnSearchSale;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnProcessRefund;
        private System.Windows.Forms.Label lblSaleID;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.DataGridView dgvSaleItems;
        private System.Windows.Forms.Button btnBack;
    }
}
