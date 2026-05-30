using System.Drawing;
using System.Windows.Forms;

namespace POSApp.Forms
{
    partial class SalesHistoryForm
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
            this.dgvSales = new System.Windows.Forms.DataGridView();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.txtSearchID = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            //
            // dgvSales
            //
            this.dgvSales.AllowUserToAddRows = false;
            this.dgvSales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSales.Location = new System.Drawing.Point(20, 110);
            this.dgvSales.Name = "dgvSales";
            this.dgvSales.ReadOnly = true;
            this.dgvSales.Size = new System.Drawing.Size(800, 250);
            this.dgvSales.TabIndex = 2;
            this.dgvSales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSales_CellClick);
            //
            // dgvDetails
            //
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.Location = new System.Drawing.Point(20, 410);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.Size = new System.Drawing.Size(800, 200);
            this.dgvDetails.TabIndex = 4;
            //
            // txtSearchID
            //
            this.txtSearchID.Location = new System.Drawing.Point(0, 5);
            this.txtSearchID.Name = "txtSearchID";
            this.txtSearchID.PlaceholderText = "Enter Sale ID";
            this.txtSearchID.Size = new System.Drawing.Size(150, 23);
            this.txtSearchID.TabIndex = 0;
            //
            // btnSearch
            //
            this.btnSearch.Location = new System.Drawing.Point(160, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(250, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 25);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(50, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(154, 30);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Sales Records";
            //
            // lblDetailTitle
            //
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Location = new System.Drawing.Point(20, 380);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(99, 21);
            this.lblDetailTitle.TabIndex = 6;
            this.lblDetailTitle.Text = "Sale Details";
            //
            // pnlSearch
            //
            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearchID);
            this.pnlSearch.Location = new System.Drawing.Point(20, 60);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(400, 40);
            this.pnlSearch.TabIndex = 7;
            //
            // btnBack
            //
            this.btnBack.Location = new System.Drawing.Point(5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 25);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            //
            // SalesHistoryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 650);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.lblDetailTitle);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.dgvSales);
            this.Controls.Add(this.lblTitle);
            this.Name = "SalesHistoryForm";
            this.Text = "Sales History";
            this.Load += new System.EventHandler(this.SalesHistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSales;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.TextBox txtSearchID;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnBack;
    }
}
