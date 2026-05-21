using System;
using System.Windows.Forms;

namespace POSApp.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            POSForm posForm = new POSForm();
            posForm.Show();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm = new SupplierForm();
            supplierForm.Show();
        }
    }
}
