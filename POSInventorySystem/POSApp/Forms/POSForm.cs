using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Forms
{
    public partial class POSForm : Form
    {
        private DatabaseHelper dbHelper;
        private List<SaleItem> cart = new List<SaleItem>();
        private decimal total = 0;

        public POSForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            UpdateCartGrid();
        }

        private void LoadProducts()
        {
            try
            {
                DataTable dt = dbHelper.ExecuteQuery("SELECT ProductID, ProductName, Price, StockQuantity FROM Products WHERE StockQuantity > 0");
                cmbProducts.DataSource = dt;
                cmbProducts.DisplayMember = "ProductName";
                cmbProducts.ValueMember = "ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedValue == null) return;

            DataRowView selectedProduct = (DataRowView)cmbProducts.SelectedItem;
            int productId = Convert.ToInt32(selectedProduct["ProductID"]);
            string productName = selectedProduct["ProductName"].ToString()!;
            decimal price = Convert.ToDecimal(selectedProduct["Price"]);
            int quantity = (int)numQuantity.Value;
            int stock = Convert.ToInt32(selectedProduct["StockQuantity"]);

            if (quantity > stock)
            {
                MessageBox.Show("Not enough stock available.");
                return;
            }

            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > stock)
                {
                    MessageBox.Show("Total quantity in cart exceeds available stock.");
                    return;
                }
                existingItem.Quantity += quantity;
                existingItem.Subtotal = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                cart.Add(new SaleItem
                {
                    ProductID = productId,
                    ProductName = productName,
                    Quantity = quantity,
                    UnitPrice = price,
                    Subtotal = quantity * price
                });
            }

            UpdateCartGrid();
        }

        private void UpdateCartGrid()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = cart.Select(i => new {
                i.ProductName,
                i.Quantity,
                i.UnitPrice,
                Subtotal = i.Subtotal
            }).ToList();

            total = cart.Sum(i => i.Subtotal);
            lblTotalValue.Text = total.ToString("C");
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            cart.Clear();
            UpdateCartGrid();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            using (var conn = dbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert Sale
                        string saleQuery = "INSERT INTO Sales (TotalAmount, FinalAmount, UserID) VALUES (@total, @final, @userId); SELECT LAST_INSERT_ID();";
                        MySqlCommand saleCmd = new MySqlCommand(saleQuery, conn, trans);
                        saleCmd.Parameters.AddWithValue("@total", total);
                        saleCmd.Parameters.AddWithValue("@final", total);
                        saleCmd.Parameters.AddWithValue("@userId", (object?)Session.CurrentUser?.UserID ?? DBNull.Value);
                        int saleId = Convert.ToInt32(saleCmd.ExecuteScalar());

                        foreach (var item in cart)
                        {
                            // Insert Sale Item
                            string itemQuery = "INSERT INTO SaleItems (SaleID, ProductID, Quantity, UnitPrice, Subtotal) VALUES (@saleId, @prodId, @qty, @price, @subtotal)";
                            MySqlCommand itemCmd = new MySqlCommand(itemQuery, conn, trans);
                            itemCmd.Parameters.AddWithValue("@saleId", saleId);
                            itemCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            itemCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            itemCmd.Parameters.AddWithValue("@price", item.UnitPrice);
                            itemCmd.Parameters.AddWithValue("@subtotal", item.Subtotal);
                            itemCmd.ExecuteNonQuery();

                            // Update Stock
                            string stockQuery = "UPDATE Products SET StockQuantity = StockQuantity - @qty WHERE ProductID = @prodId";
                            MySqlCommand stockCmd = new MySqlCommand(stockQuery, conn, trans);
                            stockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                            stockCmd.Parameters.AddWithValue("@prodId", item.ProductID);
                            stockCmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        MessageBox.Show("Sale completed successfully!");
                        cart.Clear();
                        UpdateCartGrid();
                        LoadProducts();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Error during checkout: " + ex.Message);
                    }
                }
            }
        }
    }
}
