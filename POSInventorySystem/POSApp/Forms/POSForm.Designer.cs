namespace POSApp.Forms
{
    partial class POSForm
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.pnlCategories = new System.Windows.Forms.Panel();
            this.flpCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.flpProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.pnlCartTotals = new System.Windows.Forms.Panel();
            this.lblTotalUSD = new System.Windows.Forms.Label();
            this.lblTotalLBP = new System.Windows.Forms.Label();
            this.lblTotalUSDLabel = new System.Windows.Forms.Label();
            this.lblTotalLBPLabel = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.lblPhoneLabel = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlCategories.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartTotals.SuspendLayout();
            this.pnlCustomerInfo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTop
            //
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.pnlTop.Controls.Add(this.btnBack);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.txtBarcode);
            this.pnlTop.Controls.Add(this.lblBarcode);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1280, 60);
            this.pnlTop.TabIndex = 0;
            //
            // btnBack
            //
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(60, 60);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(60, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(166, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "POS TERMINAL";
            //
            // txtBarcode
            //
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBarcode.Location = new System.Drawing.Point(950, 15);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(300, 29);
            this.txtBarcode.TabIndex = 1;
            //
            // lblBarcode
            //
            this.lblBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.ForeColor = System.Drawing.Color.White;
            this.lblBarcode.Location = new System.Drawing.Point(860, 22);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(84, 15);
            this.lblBarcode.TabIndex = 2;
            this.lblBarcode.Text = "Barcode Scan:";
            //
            // pnlCategories
            //
            this.pnlCategories.BackColor = System.Drawing.Color.White;
            this.pnlCategories.Controls.Add(this.flpCategories);
            this.pnlCategories.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCategories.Location = new System.Drawing.Point(0, 60);
            this.pnlCategories.Name = "pnlCategories";
            this.pnlCategories.Size = new System.Drawing.Size(880, 60);
            this.pnlCategories.TabIndex = 1;
            //
            // flpCategories
            //
            this.flpCategories.AutoScroll = true;
            this.flpCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCategories.Location = new System.Drawing.Point(0, 0);
            this.flpCategories.Name = "flpCategories";
            this.flpCategories.Padding = new System.Windows.Forms.Padding(5);
            this.flpCategories.Size = new System.Drawing.Size(880, 60);
            this.flpCategories.TabIndex = 0;
            this.flpCategories.WrapContents = false;
            //
            // pnlMain
            //
            this.pnlMain.Controls.Add(this.flpProducts);
            this.pnlMain.Controls.Add(this.pnlCategories);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(880, 720);
            this.pnlMain.TabIndex = 2;
            //
            // flpProducts
            //
            this.flpProducts.AutoScroll = true;
            this.flpProducts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProducts.Location = new System.Drawing.Point(0, 120);
            this.flpProducts.Name = "flpProducts";
            this.flpProducts.Padding = new System.Windows.Forms.Padding(10);
            this.flpProducts.Size = new System.Drawing.Size(880, 600);
            this.flpProducts.TabIndex = 2;
            //
            // pnlRight
            //
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.dgvCart);
            this.pnlRight.Controls.Add(this.pnlCartTotals);
            this.pnlRight.Controls.Add(this.pnlCustomerInfo);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(880, 60);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(400, 660);
            this.pnlRight.TabIndex = 3;
            //
            // dgvCart
            //
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.White;
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCart.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(0, 100);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(398, 308);
            this.dgvCart.TabIndex = 0;
            //
            // pnlCartTotals
            //
            this.pnlCartTotals.Controls.Add(this.lblTotalUSD);
            this.pnlCartTotals.Controls.Add(this.lblTotalLBP);
            this.pnlCartTotals.Controls.Add(this.lblTotalUSDLabel);
            this.pnlCartTotals.Controls.Add(this.lblTotalLBPLabel);
            this.pnlCartTotals.Controls.Add(this.btnCheckout);
            this.pnlCartTotals.Controls.Add(this.btnClearCart);
            this.pnlCartTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartTotals.Location = new System.Drawing.Point(0, 408);
            this.pnlCartTotals.Name = "pnlCartTotals";
            this.pnlCartTotals.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCartTotals.Size = new System.Drawing.Size(398, 250);
            this.pnlCartTotals.TabIndex = 1;
            //
            // lblTotalUSD
            //
            this.lblTotalUSD.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotalUSD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(70)))));
            this.lblTotalUSD.Location = new System.Drawing.Point(150, 60);
            this.lblTotalUSD.Name = "lblTotalUSD";
            this.lblTotalUSD.Size = new System.Drawing.Size(240, 45);
            this.lblTotalUSD.TabIndex = 3;
            this.lblTotalUSD.Text = "$ 0.00";
            this.lblTotalUSD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTotalLBP
            //
            this.lblTotalLBP.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalLBP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblTotalLBP.Location = new System.Drawing.Point(150, 10);
            this.lblTotalLBP.Name = "lblTotalLBP";
            this.lblTotalLBP.Size = new System.Drawing.Size(240, 40);
            this.lblTotalLBP.TabIndex = 1;
            this.lblTotalLBP.Text = "0 LBP";
            this.lblTotalLBP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblTotalUSDLabel
            //
            this.lblTotalUSDLabel.AutoSize = true;
            this.lblTotalUSDLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTotalUSDLabel.Location = new System.Drawing.Point(10, 75);
            this.lblTotalUSDLabel.Name = "lblTotalUSDLabel";
            this.lblTotalUSDLabel.Size = new System.Drawing.Size(81, 21);
            this.lblTotalUSDLabel.TabIndex = 2;
            this.lblTotalUSDLabel.Text = "Total USD:";
            //
            // lblTotalLBPLabel
            //
            this.lblTotalLBPLabel.AutoSize = true;
            this.lblTotalLBPLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTotalLBPLabel.Location = new System.Drawing.Point(10, 25);
            this.lblTotalLBPLabel.Name = "lblTotalLBPLabel";
            this.lblTotalLBPLabel.Size = new System.Drawing.Size(77, 21);
            this.lblTotalLBPLabel.TabIndex = 0;
            this.lblTotalLBPLabel.Text = "Total LBP:";
            //
            // btnCheckout
            //
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(70)))));
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(10, 120);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(378, 80);
            this.btnCheckout.TabIndex = 4;
            this.btnCheckout.Text = "CHECKOUT (F10)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            //
            // btnClearCart
            //
            this.btnClearCart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClearCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnClearCart.Location = new System.Drawing.Point(10, 200);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(378, 40);
            this.btnClearCart.TabIndex = 5;
            this.btnClearCart.Text = "Clear Cart (F12)";
            this.btnClearCart.UseVisualStyleBackColor = true;
            //
            // pnlCustomerInfo
            //
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerName);
            this.pnlCustomerInfo.Controls.Add(this.txtCustomerPhone);
            this.pnlCustomerInfo.Controls.Add(this.lblPhoneLabel);
            this.pnlCustomerInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(398, 100);
            this.pnlCustomerInfo.TabIndex = 2;
            //
            // lblCustomerName
            //
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Location = new System.Drawing.Point(15, 65);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(117, 19);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Walk-in Guest...";
            //
            // txtCustomerPhone
            //
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCustomerPhone.Location = new System.Drawing.Point(15, 30);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new System.Drawing.Size(360, 29);
            this.txtCustomerPhone.TabIndex = 1;
            //
            // lblPhoneLabel
            //
            this.lblPhoneLabel.AutoSize = true;
            this.lblPhoneLabel.Location = new System.Drawing.Point(15, 10);
            this.lblPhoneLabel.Name = "lblPhoneLabel";
            this.lblPhoneLabel.Size = new System.Drawing.Size(126, 15);
            this.lblPhoneLabel.TabIndex = 0;
            this.lblPhoneLabel.Text = "Customer Phone (F1):";
            //
            // POSForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Terminal";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCategories.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartTotals.ResumeLayout(false);
            this.pnlCartTotals.PerformLayout();
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlCategories;
        private System.Windows.Forms.FlowLayoutPanel flpCategories;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flpProducts;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlCartTotals;
        private System.Windows.Forms.Label lblTotalUSD;
        private System.Windows.Forms.Label lblTotalLBP;
        private System.Windows.Forms.Label lblTotalUSDLabel;
        private System.Windows.Forms.Label lblTotalLBPLabel;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Panel pnlCustomerInfo;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblPhoneLabel;
    }
}
