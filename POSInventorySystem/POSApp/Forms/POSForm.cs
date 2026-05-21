using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;

namespace POSApp.Forms
{
    public partial class POSForm : Form
    {
        private readonly SaleService _saleService;
        private readonly CustomerService _customerService;
        private readonly PromotionService _promotionService;
        private List<SaleItem> cart = new List<SaleItem>();
        private decimal total = 0;
        private decimal discount = 0;

        public POSForm()
        {
            InitializeComponent();
            _saleService = new SaleService();
            _customerService = new CustomerService();
            _promotionService = new PromotionService();
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCustomers();
            UpdateCartGrid();
        }

        private void LoadProducts()
        {
            try
            {
                cmbProducts.DataSource = _saleService.GetAvailableProducts();
                cmbProducts.DisplayMember = "ProductName";
                cmbProducts.ValueMember = "ProductID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var dt = _customerService.GetAllCustomers();
                DataRow dr = dt.NewRow();
                dr["CustomerID"] = DBNull.Value;
                dr["CustomerName"] = "Walk-in Customer";
                dt.Rows.InsertAt(dr, 0);

                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
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
            lblSubtotalValue.Text = total.ToString("C");

            // Auto-calculate promotions
            int? customerId = null;
            if (cmbCustomer.SelectedValue != DBNull.Value && cmbCustomer.SelectedValue != null)
                customerId = Convert.ToInt32(cmbCustomer.SelectedValue);

            decimal autoDiscount = _promotionService.CalculateDiscount(cart, customerId);
            decimal manualDiscount = 0;
            decimal.TryParse(txtDiscount.Text, out manualDiscount);

            discount = autoDiscount + manualDiscount;

            decimal finalTotal = total - discount;
            if (finalTotal < 0) finalTotal = 0;

            lblTotalValue.Text = finalTotal.ToString("C");
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateCartGrid();
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            cart.Clear();
            UpdateCartGrid();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            try
            {
                int? customerId = null;
                if (cmbCustomer.SelectedValue != DBNull.Value && cmbCustomer.SelectedValue != null)
                {
                    customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                }

                Sale sale = new Sale
                {
                    CustomerID = customerId,
                    TotalAmount = total,
                    DiscountAmount = discount,
                    FinalAmount = total - discount > 0 ? total - discount : 0,
                    Items = cart
                };

                if (_saleService.ProcessSale(sale))
                {
                    MessageBox.Show("Sale completed successfully!");
                    cart.Clear();
                    txtDiscount.Text = "0";
                    UpdateCartGrid();
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during checkout: " + ex.Message);
            }
        }
    }
}
