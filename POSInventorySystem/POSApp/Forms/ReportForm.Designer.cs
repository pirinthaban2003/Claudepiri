using System.ComponentModel;

namespace POSApp.Forms
{
    partial class ReportForm
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
            this.dgvReports = new System.Windows.Forms.DataGridView();
            this.btnDailySales = new System.Windows.Forms.Button();
            this.btnLowStock = new System.Windows.Forms.Button();
            this.btnTopProducts = new System.Windows.Forms.Button();
            this.lblReportTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).BeginInit();
            this.SuspendLayout();
            //
            // dgvReports
            //
            this.dgvReports.AllowUserToAddRows = false;
            this.dgvReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReports.Location = new System.Drawing.Point(20, 100);
            this.dgvReports.Name = "dgvReports";
            this.dgvReports.ReadOnly = true;
            this.dgvReports.Size = new System.Drawing.Size(760, 330);
            this.dgvReports.TabIndex = 0;
            //
            // btnDailySales
            //
            this.btnDailySales.Location = new System.Drawing.Point(20, 60);
            this.btnDailySales.Name = "btnDailySales";
            this.btnDailySales.Size = new System.Drawing.Size(120, 30);
            this.btnDailySales.TabIndex = 1;
            this.btnDailySales.Text = "Daily Sales";
            this.btnDailySales.UseVisualStyleBackColor = true;
            this.btnDailySales.Click += new System.EventHandler(this.btnDailySales_Click);
            //
            // btnLowStock
            //
            this.btnLowStock.Location = new System.Drawing.Point(150, 60);
            this.btnLowStock.Name = "btnLowStock";
            this.btnLowStock.Size = new System.Drawing.Size(120, 30);
            this.btnLowStock.TabIndex = 2;
            this.btnLowStock.Text = "Low Stock";
            this.btnLowStock.UseVisualStyleBackColor = true;
            this.btnLowStock.Click += new System.EventHandler(this.btnLowStock_Click);
            //
            // btnTopProducts
            //
            this.btnTopProducts.Location = new System.Drawing.Point(280, 60);
            this.btnTopProducts.Name = "btnTopProducts";
            this.btnTopProducts.Size = new System.Drawing.Size(120, 30);
            this.btnTopProducts.TabIndex = 3;
            this.btnTopProducts.Text = "Top Products";
            this.btnTopProducts.UseVisualStyleBackColor = true;
            this.btnTopProducts.Click += new System.EventHandler(this.btnTopProducts_Click);
            //
            // lblReportTitle
            //
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReportTitle.Location = new System.Drawing.Point(20, 15);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(242, 32);
            this.lblReportTitle.TabIndex = 4;
            this.lblReportTitle.Text = "Reports && Analytics";
            //
            // ReportForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.btnTopProducts);
            this.Controls.Add(this.btnLowStock);
            this.Controls.Add(this.btnDailySales);
            this.Controls.Add(this.dgvReports);
            this.Name = "ReportForm";
            this.Text = "SRMS Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvReports;
        private System.Windows.Forms.Button btnDailySales;
        private System.Windows.Forms.Button btnLowStock;
        private System.Windows.Forms.Button btnTopProducts;
        private System.Windows.Forms.Label lblReportTitle;
    }
}
