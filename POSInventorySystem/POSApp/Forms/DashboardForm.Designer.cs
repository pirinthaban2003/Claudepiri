using System.ComponentModel;

namespace POSApp.Forms
{
    partial class DashboardForm
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlLowStock = new System.Windows.Forms.Panel();
            this.lblLowStockCount = new System.Windows.Forms.Label();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.pnlSalesToday = new System.Windows.Forms.Panel();
            this.lblSalesTodayAmount = new System.Windows.Forms.Label();
            this.lblSalesTodayTitle = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLowStock.SuspendLayout();
            this.pnlSalesToday.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlSidebar
            //
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnCustomers);
            this.pnlSidebar.Controls.Add(this.btnSuppliers);
            this.pnlSidebar.Controls.Add(this.btnInventory);
            this.pnlSidebar.Controls.Add(this.btnPOS);
            this.pnlSidebar.Controls.Add(this.lblTitle);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 600);
            this.pnlSidebar.TabIndex = 0;
            //
            // btnReports
            //
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(0, 260);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(200, 40);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "Reports & Analytics";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            //
            // btnCustomers
            //
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.ForeColor = System.Drawing.Color.White;
            this.btnCustomers.Location = new System.Drawing.Point(0, 220);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(200, 40);
            this.btnCustomers.TabIndex = 4;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            //
            // btnSuppliers
            //
            this.btnSuppliers.FlatAppearance.BorderSize = 0;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.ForeColor = System.Drawing.Color.White;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 180);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(200, 40);
            this.btnSuppliers.TabIndex = 3;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            //
            // btnInventory
            //
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(0, 140);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(200, 40);
            this.btnInventory.TabIndex = 2;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            //
            // btnPOS
            //
            this.btnPOS.FlatAppearance.BorderSize = 0;
            this.btnPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPOS.ForeColor = System.Drawing.Color.White;
            this.btnPOS.Location = new System.Drawing.Point(0, 100);
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Size = new System.Drawing.Size(200, 40);
            this.btnPOS.TabIndex = 1;
            this.btnPOS.Text = "POS Terminal";
            this.btnPOS.UseVisualStyleBackColor = true;
            this.btnPOS.Click += new System.EventHandler(this.btnPOS_Click);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SRMS Admin";
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(200, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 60);
            this.pnlHeader.TabIndex = 1;
            //
            // lblWelcome
            //
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(121, 21);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, User!";
            //
            // pnlContent
            //
            this.pnlContent.Controls.Add(this.pnlLowStock);
            this.pnlContent.Controls.Add(this.pnlSalesToday);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(200, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 540);
            this.pnlContent.TabIndex = 2;
            //
            // pnlLowStock
            //
            this.pnlLowStock.BackColor = System.Drawing.Color.Tomato;
            this.pnlLowStock.Controls.Add(this.lblLowStockCount);
            this.pnlLowStock.Controls.Add(this.lblLowStockTitle);
            this.pnlLowStock.Location = new System.Drawing.Point(300, 30);
            this.pnlLowStock.Name = "pnlLowStock";
            this.pnlLowStock.Size = new System.Drawing.Size(250, 120);
            this.pnlLowStock.TabIndex = 1;
            //
            // lblLowStockCount
            //
            this.lblLowStockCount.AutoSize = true;
            this.lblLowStockCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLowStockCount.ForeColor = System.Drawing.Color.White;
            this.lblLowStockCount.Location = new System.Drawing.Point(20, 45);
            this.lblLowStockCount.Name = "lblLowStockCount";
            this.lblLowStockCount.Size = new System.Drawing.Size(38, 45);
            this.lblLowStockCount.TabIndex = 1;
            this.lblLowStockCount.Text = "0";
            //
            // lblLowStockTitle
            //
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.White;
            this.lblLowStockTitle.Location = new System.Drawing.Point(20, 20);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(91, 15);
            this.lblLowStockTitle.TabIndex = 0;
            this.lblLowStockTitle.Text = "Low Stock Items";
            //
            // pnlSalesToday
            //
            this.pnlSalesToday.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.pnlSalesToday.Controls.Add(this.lblSalesTodayAmount);
            this.pnlSalesToday.Controls.Add(this.lblSalesTodayTitle);
            this.pnlSalesToday.Location = new System.Drawing.Point(30, 30);
            this.pnlSalesToday.Name = "pnlSalesToday";
            this.pnlSalesToday.Size = new System.Drawing.Size(250, 120);
            this.pnlSalesToday.TabIndex = 0;
            //
            // lblSalesTodayAmount
            //
            this.lblSalesTodayAmount.AutoSize = true;
            this.lblSalesTodayAmount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSalesTodayAmount.ForeColor = System.Drawing.Color.White;
            this.lblSalesTodayAmount.Location = new System.Drawing.Point(20, 45);
            this.lblSalesTodayAmount.Name = "lblSalesTodayAmount";
            this.lblSalesTodayAmount.Size = new System.Drawing.Size(103, 45);
            this.lblSalesTodayAmount.TabIndex = 1;
            this.lblSalesTodayAmount.Text = "$0.00";
            //
            // lblSalesTodayTitle
            //
            this.lblSalesTodayTitle.AutoSize = true;
            this.lblSalesTodayTitle.ForeColor = System.Drawing.Color.White;
            this.lblSalesTodayTitle.Location = new System.Drawing.Point(20, 20);
            this.lblSalesTodayTitle.Name = "lblSalesTodayTitle";
            this.lblSalesTodayTitle.Size = new System.Drawing.Size(78, 15);
            this.lblSalesTodayTitle.TabIndex = 0;
            this.lblSalesTodayTitle.Text = "Sales (Today)";
            //
            // DashboardForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SRMS Dashboard";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlLowStock.ResumeLayout(false);
            this.pnlLowStock.PerformLayout();
            this.pnlSalesToday.ResumeLayout(false);
            this.pnlSalesToday.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnPOS;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlLowStock;
        private System.Windows.Forms.Label lblLowStockCount;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Panel pnlSalesToday;
        private System.Windows.Forms.Label lblSalesTodayAmount;
        private System.Windows.Forms.Label lblSalesTodayTitle;
    }
}
