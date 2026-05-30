using System.ComponentModel;

namespace POSApp.Forms
{
    partial class CustomerForm
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
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.txtWallet = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnViewHistory = new System.Windows.Forms.Button();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblWallet = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            //
            // dgvCustomers
            //
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.Location = new System.Drawing.Point(10, 50);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.Size = new System.Drawing.Size(660, 460);
            this.dgvCustomers.TabIndex = 0;
            this.dgvCustomers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomers_CellClick);
            //
            // pnlForm
            //
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlForm.Controls.Add(this.btnBack);
            this.pnlForm.Controls.Add(this.txtLevel);
            this.pnlForm.Controls.Add(this.lblLevel);
            this.pnlForm.Controls.Add(this.lblWallet);
            this.pnlForm.Controls.Add(this.lblPoints);
            this.pnlForm.Controls.Add(this.lblEmail);
            this.pnlForm.Controls.Add(this.lblPhone);
            this.pnlForm.Controls.Add(this.lblCustomerName);
            this.pnlForm.Controls.Add(this.btnViewHistory);
            this.pnlForm.Controls.Add(this.btnClear);
            this.pnlForm.Controls.Add(this.btnDelete);
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.txtWallet);
            this.pnlForm.Controls.Add(this.txtPoints);
            this.pnlForm.Controls.Add(this.txtEmail);
            this.pnlForm.Controls.Add(this.txtPhone);
            this.pnlForm.Controls.Add(this.txtCustomerName);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(300, 520);
            this.pnlForm.TabIndex = 16;
            //
            // pnlGrid
            //
            this.pnlGrid.Controls.Add(this.dgvCustomers);
            this.pnlGrid.Controls.Add(this.txtSearch);
            this.pnlGrid.Controls.Add(this.lblSearch);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(300, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(680, 520);
            this.pnlGrid.TabIndex = 17;
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
            // txtCustomerName
            //
            this.txtCustomerName.Location = new System.Drawing.Point(120, 40);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(160, 23);
            this.txtCustomerName.TabIndex = 1;
            //
            // txtPhone
            //
            this.txtPhone.Location = new System.Drawing.Point(120, 75);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(160, 23);
            this.txtPhone.TabIndex = 2;
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(120, 110);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(160, 23);
            this.txtEmail.TabIndex = 3;
            //
            // txtPoints
            //
            this.txtPoints.Location = new System.Drawing.Point(120, 145);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(160, 23);
            this.txtPoints.TabIndex = 4;
            //
            // txtWallet
            //
            this.txtWallet.Location = new System.Drawing.Point(120, 180);
            this.txtWallet.Name = "txtWallet";
            this.txtWallet.Size = new System.Drawing.Size(160, 23);
            this.txtWallet.TabIndex = 5;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(20, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "💾 Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnDelete
            //
            this.btnDelete.Location = new System.Drawing.Point(110, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 35);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "🗑 Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(200, 280);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 35);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "🧹 Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // btnViewHistory
            //
            this.btnViewHistory.Location = new System.Drawing.Point(20, 325);
            this.btnViewHistory.Name = "btnViewHistory";
            this.btnViewHistory.Size = new System.Drawing.Size(260, 35);
            this.btnViewHistory.TabIndex = 23;
            this.btnViewHistory.Text = "📜 View Sales History";
            this.btnViewHistory.UseVisualStyleBackColor = true;
            this.btnViewHistory.Click += new System.EventHandler(this.btnViewHistory_Click);
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
            // lblCustomerName
            //
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(20, 43);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(115, 15);
            this.lblCustomerName.TabIndex = 9;
            this.lblCustomerName.Text = "Customer Name:";
            //
            // lblPhone
            //
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 78);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(44, 15);
            this.lblPhone.TabIndex = 10;
            this.lblPhone.Text = "Phone:";
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 113);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 11;
            this.lblEmail.Text = "Email:";
            //
            // lblPoints
            //
            this.lblPoints.AutoSize = true;
            this.lblPoints.Location = new System.Drawing.Point(20, 148);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(43, 15);
            this.lblPoints.TabIndex = 12;
            this.lblPoints.Text = "Points:";
            //
            // lblWallet
            //
            this.lblWallet.AutoSize = true;
            this.lblWallet.Location = new System.Drawing.Point(20, 183);
            this.lblWallet.Name = "lblWallet";
            this.lblWallet.Size = new System.Drawing.Size(43, 15);
            this.lblWallet.TabIndex = 13;
            this.lblWallet.Text = "Wallet:";
            //
            // lblLevel
            //
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(20, 218);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(37, 15);
            this.lblLevel.TabIndex = 14;
            this.lblLevel.Text = "Level:";
            //
            // txtLevel
            //
            this.txtLevel.Location = new System.Drawing.Point(120, 215);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.ReadOnly = true;
            this.txtLevel.Size = new System.Drawing.Size(160, 23);
            this.txtLevel.TabIndex = 15;
            //
            // CustomerForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 520);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlForm);
            this.Name = "CustomerForm";
            this.Text = "Customer Management";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvCustomers;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.TextBox txtWallet;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblWallet;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnBack;
    }
}
