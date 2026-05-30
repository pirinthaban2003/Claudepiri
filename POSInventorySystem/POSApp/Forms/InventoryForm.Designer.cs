namespace POSApp.Forms
{
    partial class InventoryForm
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
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnManageBatches = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblSKU = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblUnitType = new System.Windows.Forms.Label();
            this.cmbUnitType = new System.Windows.Forms.ComboBox();
            this.lblTaxCategory = new System.Windows.Forms.Label();
            this.cmbTaxCategory = new System.Windows.Forms.ComboBox();
            this.lblMinStock = new System.Windows.Forms.Label();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.lblDiscountRate = new System.Windows.Forms.Label();
            this.txtDiscountRate = new System.Windows.Forms.TextBox();
            this.chkIsBOGO = new System.Windows.Forms.CheckBox();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            //
            // dgvProducts
            //
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProducts.Location = new System.Drawing.Point(10, 50);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.Size = new System.Drawing.Size(660, 540);
            this.dgvProducts.TabIndex = 0;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            //
            // pnlForm
            //
            this.pnlForm.Controls.Add(this.btnBack);
            this.pnlForm.Controls.Add(this.btnAddCategory);
            this.pnlForm.Controls.Add(this.chkIsBOGO);
            this.pnlForm.Controls.Add(this.lblDiscountRate);
            this.pnlForm.Controls.Add(this.txtDiscountRate);
            this.pnlForm.Controls.Add(this.lblMinStock);
            this.pnlForm.Controls.Add(this.txtMinStock);
            this.pnlForm.Controls.Add(this.lblTaxCategory);
            this.pnlForm.Controls.Add(this.cmbTaxCategory);
            this.pnlForm.Controls.Add(this.lblUnitType);
            this.pnlForm.Controls.Add(this.cmbUnitType);
            this.pnlForm.Controls.Add(this.lblSupplier);
            this.pnlForm.Controls.Add(this.lblBrand);
            this.pnlForm.Controls.Add(this.lblSKU);
            this.pnlForm.Controls.Add(this.lblBarcode);
            this.pnlForm.Controls.Add(this.lblCategory);
            this.pnlForm.Controls.Add(this.lblQuantity);
            this.pnlForm.Controls.Add(this.lblPrice);
            this.pnlForm.Controls.Add(this.lblProductName);
            this.pnlForm.Controls.Add(this.btnClear);
            this.pnlForm.Controls.Add(this.btnManageBatches);
            this.pnlForm.Controls.Add(this.btnDelete);
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.cmbSupplier);
            this.pnlForm.Controls.Add(this.cmbCategory);
            this.pnlForm.Controls.Add(this.txtBrand);
            this.pnlForm.Controls.Add(this.txtSKU);
            this.pnlForm.Controls.Add(this.txtBarcode);
            this.pnlForm.Controls.Add(this.txtQuantity);
            this.pnlForm.Controls.Add(this.txtPrice);
            this.pnlForm.Controls.Add(this.txtProductName);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(300, 600);
            this.pnlForm.TabIndex = 0;
            //
            // btnBack
            //
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 25);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // txtProductName
            //
            this.txtProductName.Location = new System.Drawing.Point(120, 40);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(160, 23);
            this.txtProductName.TabIndex = 1;
            //
            // lblProductName
            //
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(20, 43);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(87, 15);
            this.lblProductName.TabIndex = 12;
            this.lblProductName.Text = "Product Name:";
            //
            // txtSKU
            //
            this.txtSKU.Location = new System.Drawing.Point(120, 75);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.Size = new System.Drawing.Size(160, 23);
            this.txtSKU.TabIndex = 2;
            //
            // lblSKU
            //
            this.lblSKU.AutoSize = true;
            this.lblSKU.Location = new System.Drawing.Point(20, 78);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(31, 15);
            this.lblSKU.TabIndex = 17;
            this.lblSKU.Text = "SKU:";
            //
            // txtBarcode
            //
            this.txtBarcode.Location = new System.Drawing.Point(120, 110);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(160, 23);
            this.txtBarcode.TabIndex = 3;
            //
            // lblBarcode
            //
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(20, 113);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(53, 15);
            this.lblBarcode.TabIndex = 16;
            this.lblBarcode.Text = "Barcode:";
            //
            // cmbCategory
            //
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(120, 145);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(130, 23);
            this.cmbCategory.TabIndex = 4;
            //
            // lblCategory
            //
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 148);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(58, 15);
            this.lblCategory.TabIndex = 15;
            this.lblCategory.Text = "Category:";
            //
            // btnAddCategory
            //
            this.btnAddCategory.Location = new System.Drawing.Point(255, 145);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(25, 23);
            this.btnAddCategory.TabIndex = 31;
            this.btnAddCategory.Text = "+";
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            //
            // txtBrand
            //
            this.txtBrand.Location = new System.Drawing.Point(120, 180);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(160, 23);
            this.txtBrand.TabIndex = 5;
            //
            // lblBrand
            //
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(20, 183);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(41, 15);
            this.lblBrand.TabIndex = 18;
            this.lblBrand.Text = "Brand:";
            //
            // cmbSupplier
            //
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(120, 215);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(160, 23);
            this.cmbSupplier.TabIndex = 6;
            //
            // lblSupplier
            //
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(20, 218);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(53, 15);
            this.lblSupplier.TabIndex = 19;
            this.lblSupplier.Text = "Supplier:";
            //
            // cmbUnitType
            //
            this.cmbUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitType.FormattingEnabled = true;
            this.cmbUnitType.Items.AddRange(new object[] {
            "pcs",
            "kg",
            "ltr",
            "pkt",
            "box",
            "dz"});
            this.cmbUnitType.Location = new System.Drawing.Point(120, 250);
            this.cmbUnitType.Name = "cmbUnitType";
            this.cmbUnitType.Size = new System.Drawing.Size(160, 23);
            this.cmbUnitType.TabIndex = 7;
            //
            // lblUnitType
            //
            this.lblUnitType.AutoSize = true;
            this.lblUnitType.Location = new System.Drawing.Point(20, 253);
            this.lblUnitType.Name = "lblUnitType";
            this.lblUnitType.Size = new System.Drawing.Size(59, 15);
            this.lblUnitType.TabIndex = 22;
            this.lblUnitType.Text = "Unit Type:";
            //
            // cmbTaxCategory
            //
            this.cmbTaxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxCategory.FormattingEnabled = true;
            this.cmbTaxCategory.Location = new System.Drawing.Point(120, 285);
            this.cmbTaxCategory.Name = "cmbTaxCategory";
            this.cmbTaxCategory.Size = new System.Drawing.Size(160, 23);
            this.cmbTaxCategory.TabIndex = 8;
            //
            // lblTaxCategory
            //
            this.lblTaxCategory.AutoSize = true;
            this.lblTaxCategory.Location = new System.Drawing.Point(20, 288);
            this.lblTaxCategory.Name = "lblTaxCategory";
            this.lblTaxCategory.Size = new System.Drawing.Size(78, 15);
            this.lblTaxCategory.TabIndex = 24;
            this.lblTaxCategory.Text = "Tax Category:";
            //
            // txtPrice
            //
            this.txtPrice.Location = new System.Drawing.Point(120, 320);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(160, 23);
            this.txtPrice.TabIndex = 9;
            //
            // lblPrice
            //
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(20, 323);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(36, 15);
            this.lblPrice.TabIndex = 13;
            this.lblPrice.Text = "Price:";
            //
            // txtQuantity
            //
            this.txtQuantity.Location = new System.Drawing.Point(120, 355);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(60, 23);
            this.txtQuantity.TabIndex = 10;
            //
            // lblQuantity
            //
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(20, 358);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblQuantity.TabIndex = 14;
            this.lblQuantity.Text = "Quantity:";
            //
            // txtMinStock
            //
            this.txtMinStock.Location = new System.Drawing.Point(220, 355);
            this.txtMinStock.Name = "txtMinStock";
            this.txtMinStock.Size = new System.Drawing.Size(60, 23);
            this.txtMinStock.TabIndex = 30;
            this.txtMinStock.Text = "10";
            //
            // lblMinStock
            //
            this.lblMinStock.AutoSize = true;
            this.lblMinStock.Location = new System.Drawing.Point(185, 358);
            this.lblMinStock.Name = "lblMinStock";
            this.lblMinStock.Size = new System.Drawing.Size(31, 15);
            this.lblMinStock.TabIndex = 29;
            this.lblMinStock.Text = "Min:";
            //
            // txtDiscountRate
            //
            this.txtDiscountRate.Location = new System.Drawing.Point(120, 390);
            this.txtDiscountRate.Name = "txtDiscountRate";
            this.txtDiscountRate.Size = new System.Drawing.Size(60, 23);
            this.txtDiscountRate.TabIndex = 11;
            this.txtDiscountRate.Text = "0";
            //
            // lblDiscountRate
            //
            this.lblDiscountRate.AutoSize = true;
            this.lblDiscountRate.Location = new System.Drawing.Point(20, 393);
            this.lblDiscountRate.Name = "lblDiscountRate";
            this.lblDiscountRate.Size = new System.Drawing.Size(73, 15);
            this.lblDiscountRate.TabIndex = 26;
            this.lblDiscountRate.Text = "Perm Disc %:";
            //
            // chkIsBOGO
            //
            this.chkIsBOGO.AutoSize = true;
            this.chkIsBOGO.ForeColor = System.Drawing.Color.DimGray;
            this.chkIsBOGO.Location = new System.Drawing.Point(190, 392);
            this.chkIsBOGO.Name = "chkIsBOGO";
            this.chkIsBOGO.Size = new System.Drawing.Size(59, 19);
            this.chkIsBOGO.TabIndex = 28;
            this.chkIsBOGO.Text = "BOGO";
            this.chkIsBOGO.UseVisualStyleBackColor = true;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(20, 435);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 35);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "💾 Save (F2)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnManageBatches
            //
            this.btnManageBatches.Location = new System.Drawing.Point(155, 435);
            this.btnManageBatches.Name = "btnManageBatches";
            this.btnManageBatches.Size = new System.Drawing.Size(125, 35);
            this.btnManageBatches.TabIndex = 32;
            this.btnManageBatches.Text = "📦 Batches";
            this.btnManageBatches.UseVisualStyleBackColor = true;
            this.btnManageBatches.Click += new System.EventHandler(this.btnManageBatches_Click);
            //
            // btnDelete
            //
            this.btnDelete.Location = new System.Drawing.Point(20, 480);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(125, 35);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Text = "🗑 Delete (F3)";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(155, 480);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(125, 35);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "🧹 Clear (F4)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // pnlGrid
            //
            this.pnlGrid.Controls.Add(this.dgvProducts);
            this.pnlGrid.Controls.Add(this.txtSearch);
            this.pnlGrid.Controls.Add(this.lblSearch);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(300, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(680, 600);
            this.pnlGrid.TabIndex = 21;
            //
            // txtSearch
            //
            this.txtSearch.Location = new System.Drawing.Point(100, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 1;
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(15, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(81, 15);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search (Ctrl+F):";
            //
            // InventoryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlForm);
            this.Name = "InventoryForm";
            this.Text = "Inventory Management";
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtSKU;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnManageBatches;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblSKU;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblUnitType;
        private System.Windows.Forms.ComboBox cmbUnitType;
        private System.Windows.Forms.Label lblTaxCategory;
        private System.Windows.Forms.ComboBox cmbTaxCategory;
        private System.Windows.Forms.Label lblMinStock;
        private System.Windows.Forms.TextBox txtMinStock;
        private System.Windows.Forms.Label lblDiscountRate;
        private System.Windows.Forms.TextBox txtDiscountRate;
        private System.Windows.Forms.CheckBox chkIsBOGO;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}
