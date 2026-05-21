using System;
using System.Windows.Forms;

namespace POSApp
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
    }
}
