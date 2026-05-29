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
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.cmbProducts = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblSubtotalValue = new System.Windows.Forms.Label();
            this.lblProductSearch = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblCustomerContact = new System.Windows.Forms.Label();
            this.txtCustomerContact = new System.Windows.Forms.TextBox();
            this.lblBarcodeScan = new System.Windows.Forms.Label();
            this.txtBarcodeScan = new System.Windows.Forms.TextBox();
            this.pnlQtyActions = new System.Windows.Forms.Panel();
            this.btnQtyPlus = new System.Windows.Forms.Button();
            this.btnQtyMinus = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.pnlPayment = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.pnlQtyActions.SuspendLayout();
            this.pnlCart.SuspendLayout();
            this.pnlPayment.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSearch
            //
            this.pnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearch.Controls.Add(this.lblCustomerContact);
            this.pnlSearch.Controls.Add(this.txtCustomerContact);
            this.pnlSearch.Controls.Add(this.lblBarcodeScan);
            this.pnlSearch.Controls.Add(this.txtBarcodeScan);
            this.pnlSearch.Controls.Add(this.btnResume);
            this.pnlSearch.Controls.Add(this.btnHold);
            this.pnlSearch.Controls.Add(this.lblProductSearch);
            this.pnlSearch.Controls.Add(this.txtProductSearch);
            this.pnlSearch.Controls.Add(this.lblProduct);
            this.pnlSearch.Controls.Add(this.cmbProducts);
            this.pnlSearch.Controls.Add(this.lblQuantity);
            this.pnlSearch.Controls.Add(this.pnlQtyActions);
            this.pnlSearch.Controls.Add(this.btnAddToCart);
            this.pnlSearch.Controls.Add(this.lblCustomer);
            this.pnlSearch.Controls.Add(this.cmbCustomer);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(250, 600);
            this.pnlSearch.TabIndex = 17;
            //
            // lblCustomerContact
            //
            this.lblCustomerContact.AutoSize = true;
            this.lblCustomerContact.Location = new System.Drawing.Point(20, 10);
            this.lblCustomerContact.Name = "lblCustomerContact";
            this.lblCustomerContact.Size = new System.Drawing.Size(107, 15);
            this.lblCustomerContact.TabIndex = 16;
            this.lblCustomerContact.Text = "Customer Contact (F1):";
            //
            // txtCustomerContact
            //
            this.txtCustomerContact.Location = new System.Drawing.Point(20, 30);
            this.txtCustomerContact.Name = "txtCustomerContact";
            this.txtCustomerContact.Size = new System.Drawing.Size(200, 23);
            this.txtCustomerContact.TabIndex = 17;
            this.txtCustomerContact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerContact_KeyDown);
            //
            // cmbCustomer
            //
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(20, 75);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(200, 23);
            this.cmbCustomer.TabIndex = 11;
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(20, 55);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(130, 15);
            this.lblCustomer.TabIndex = 12;
            this.lblCustomer.Text = "Select Customer (Ctrl+S):";
            //
            // lblBarcodeScan
            //
            this.lblBarcodeScan.AutoSize = true;
            this.lblBarcodeScan.Location = new System.Drawing.Point(20, 110);
            this.lblBarcodeScan.Name = "lblBarcodeScan";
            this.lblBarcodeScan.Size = new System.Drawing.Size(104, 15);
            this.lblBarcodeScan.TabIndex = 15;
            this.lblBarcodeScan.Text = "Scan Barcode (F2):";
            //
            // txtBarcodeScan
            //
            this.txtBarcodeScan.Location = new System.Drawing.Point(20, 130);
            this.txtBarcodeScan.Name = "txtBarcodeScan";
            this.txtBarcodeScan.Size = new System.Drawing.Size(200, 23);
            this.txtBarcodeScan.TabIndex = 0;
            //
            // lblProductSearch
            //
            this.lblProductSearch.AutoSize = true;
            this.lblProductSearch.Location = new System.Drawing.Point(20, 160);
            this.lblProductSearch.Name = "lblProductSearch";
            this.lblProductSearch.Size = new System.Drawing.Size(111, 15);
            this.lblProductSearch.TabIndex = 18;
            this.lblProductSearch.Text = "Product Search (F3):";
            //
            // txtProductSearch
            //
            this.txtProductSearch.Location = new System.Drawing.Point(20, 180);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(200, 23);
            this.txtProductSearch.TabIndex = 19;
            //
            // cmbProducts
            //
            this.cmbProducts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducts.FormattingEnabled = true;
            this.cmbProducts.Location = new System.Drawing.Point(20, 230);
            this.cmbProducts.Name = "cmbProducts";
            this.cmbProducts.Size = new System.Drawing.Size(200, 23);
            this.cmbProducts.TabIndex = 1;
            //
            // lblProduct
            //
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(20, 210);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(81, 15);
            this.lblProduct.TabIndex = 2;
            this.lblProduct.Text = "Select Result:";
            //
            // lblQuantity
            //
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(20, 270);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity:";
            //
            // pnlQtyActions
            //
            this.pnlQtyActions.Controls.Add(this.btnQtyPlus);
            this.pnlQtyActions.Controls.Add(this.btnQtyMinus);
            this.pnlQtyActions.Controls.Add(this.numQuantity);
            this.pnlQtyActions.Location = new System.Drawing.Point(20, 290);
            this.pnlQtyActions.Name = "pnlQtyActions";
            this.pnlQtyActions.Size = new System.Drawing.Size(200, 30);
            this.pnlQtyActions.TabIndex = 5;
            //
            // btnQtyPlus
            //
            this.btnQtyPlus.Location = new System.Drawing.Point(170, 0);
            this.btnQtyPlus.Name = "btnQtyPlus";
            this.btnQtyPlus.Size = new System.Drawing.Size(30, 30);
            this.btnQtyPlus.TabIndex = 2;
            this.btnQtyPlus.Text = "+";
            this.btnQtyPlus.UseVisualStyleBackColor = true;
            this.btnQtyPlus.Click += new System.EventHandler(this.btnQtyPlus_Click);
            //
            // btnQtyMinus
            //
            this.btnQtyMinus.Location = new System.Drawing.Point(140, 0);
            this.btnQtyMinus.Name = "btnQtyMinus";
            this.btnQtyMinus.Size = new System.Drawing.Size(30, 30);
            this.btnQtyMinus.TabIndex = 1;
            this.btnQtyMinus.Text = "-";
            this.btnQtyMinus.UseVisualStyleBackColor = true;
            this.btnQtyMinus.Click += new System.EventHandler(this.btnQtyMinus_Click);
            //
            // numQuantity
            //
            this.numQuantity.Location = new System.Drawing.Point(0, 4);
            this.numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(130, 23);
            this.numQuantity.TabIndex = 0;
            this.numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // btnAddToCart
            //
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(20, 340);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(200, 45);
            this.btnAddToCart.TabIndex = 6;
            this.btnAddToCart.Text = "🛒 ADD TO CART";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            //
            // btnResume
            //
            this.btnResume.Location = new System.Drawing.Point(20, 460);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(200, 35);
            this.btnResume.TabIndex = 14;
            this.btnResume.Text = "📂 Resume (F5)";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            //
            // btnHold
            //
            this.btnHold.Location = new System.Drawing.Point(20, 420);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(200, 35);
            this.btnHold.TabIndex = 13;
            this.btnHold.Text = "⏸ Hold Sale (F4)";
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            //
            // pnlCart
            //
            this.pnlCart.Controls.Add(this.dgvCart);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCart.Location = new System.Drawing.Point(250, 0);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Size = new System.Drawing.Size(700, 600);
            this.pnlCart.TabIndex = 18;
            //
            // pnlPayment
            //
            this.pnlPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPayment.Controls.Add(this.lblSubtotal);
            this.pnlPayment.Controls.Add(this.lblSubtotalValue);
            this.pnlPayment.Controls.Add(this.lblDiscount);
            this.pnlPayment.Controls.Add(this.txtDiscount);
            this.pnlPayment.Controls.Add(this.lblTotal);
            this.pnlPayment.Controls.Add(this.lblTotalValue);
            this.pnlPayment.Controls.Add(this.btnCheckout);
            this.pnlPayment.Controls.Add(this.btnClearCart);
            this.pnlPayment.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPayment.Location = new System.Drawing.Point(950, 0);
            this.pnlPayment.Name = "pnlPayment";
            this.pnlPayment.Size = new System.Drawing.Size(250, 600);
            this.pnlPayment.TabIndex = 19;
            //
            // dgvCart
            //
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(0, 0);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.Size = new System.Drawing.Size(700, 600);
            this.dgvCart.TabIndex = 0;
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(20, 150);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(70, 30);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total:";
            //
            // lblTotalValue
            //
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.lblTotalValue.Location = new System.Drawing.Point(20, 180);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(135, 59);
            this.lblTotalValue.TabIndex = 8;
            this.lblTotalValue.Text = "Rs. 0.00";
            //
            // btnCheckout
            //
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(10, 260);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(230, 90);
            this.btnCheckout.TabIndex = 9;
            this.btnCheckout.Text = "💳 CHECKOUT\n(F10 / Enter)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            //
            // btnClearCart
            //
            this.btnClearCart.Location = new System.Drawing.Point(10, 360);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(230, 30);
            this.btnClearCart.TabIndex = 10;
            this.btnClearCart.Text = "Clear Cart (F12)";
            this.btnClearCart.UseVisualStyleBackColor = true;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            //
            // txtDiscount
            //
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDiscount.Location = new System.Drawing.Point(20, 100);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(210, 29);
            this.txtDiscount.TabIndex = 13;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            //
            // lblDiscount
            //
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(20, 80);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(95, 15);
            this.lblDiscount.TabIndex = 14;
            this.lblDiscount.Text = "Discount (Ctrl+D):";
            //
            // lblSubtotal
            //
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(20, 30);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(54, 15);
            this.lblSubtotal.TabIndex = 15;
            this.lblSubtotal.Text = "Subtotal:";
            //
            // lblSubtotalValue
            //
            this.lblSubtotalValue.AutoSize = true;
            this.lblSubtotalValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotalValue.Location = new System.Drawing.Point(20, 50);
            this.lblSubtotalValue.Name = "lblSubtotalValue";
            this.lblSubtotalValue.Size = new System.Drawing.Size(49, 21);
            this.lblSubtotalValue.TabIndex = 16;
            this.lblSubtotalValue.Text = "Rs. 0.00";
            //
            // POSForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.pnlCart);
            this.Controls.Add(this.pnlPayment);
            this.Controls.Add(this.pnlSearch);
            this.Name = "POSForm";
            this.Text = "Supermarket POS Terminal";
            this.Load += new System.EventHandler(this.POSForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlQtyActions.ResumeLayout(false);
            this.pnlCart.ResumeLayout(false);
            this.pnlPayment.ResumeLayout(false);
            this.pnlPayment.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.ComboBox cmbProducts;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblProductSearch;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblSubtotalValue;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlCart;
        private System.Windows.Forms.Panel pnlPayment;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Label lblBarcodeScan;
        private System.Windows.Forms.TextBox txtBarcodeScan;
        private System.Windows.Forms.Label lblCustomerContact;
        private System.Windows.Forms.TextBox txtCustomerContact;
        private System.Windows.Forms.Panel pnlQtyActions;
        private System.Windows.Forms.Button btnQtyPlus;
        private System.Windows.Forms.Button btnQtyMinus;
    }
}
