using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using POSApp.Models;
using POSApp.Services;
using POSApp.Utilities;

namespace POSApp.Forms
{
    public partial class POSForm : Form
    {
        private readonly SaleService _saleService;
        private readonly CustomerService _customerService;
        private readonly PromotionService _promotionService;
        private List<SaleItem> cart = new List<SaleItem>();
        private static List<SaleItem>? heldCart = null;
        private static int? heldCustomerId = null;
        private decimal total = 0;
        private decimal autoDiscount = 0;
        private decimal taxTotal = 0;
        private decimal manualDiscount = 0;

        public POSForm()
        {
            InitializeComponent();
            _saleService = new SaleService();
            _customerService = new CustomerService();
            _promotionService = new PromotionService();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(POSForm_KeyDown);
            ThemeHelper.ApplyTheme(this);
            CustomizeComponents();
        }

        private void CustomizeComponents()
        {
            pnlSearch.BackColor = ThemeHelper.PrimaryDark;
            pnlPayment.BackColor = ThemeHelper.PrimaryDark;
            btnCheckout.BackColor = ThemeHelper.AccentBlue;
            btnAddToCart.BackColor = ThemeHelper.AccentGreen;
            lblTotalValue.ForeColor = ThemeHelper.AccentGreen;

            this.AcceptButton = btnCheckout;
            dgvCart.CellFormatting += DgvCart_CellFormatting;

            btnResume.Enabled = heldCart != null;
            txtBarcodeScan.KeyDown += TxtBarcodeScan_KeyDown;
            numQuantity.KeyDown += NumQuantity_KeyDown;

            // Auto-select text on focus for speed
            txtCustomerContact.GotFocus += (s, e) => txtCustomerContact.SelectAll();
            txtBarcodeScan.GotFocus += (s, e) => txtBarcodeScan.SelectAll();
            numQuantity.GotFocus += (s, e) => numQuantity.Select(0, numQuantity.Text.Length);
        }

        private void NumQuantity_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddToCart.PerformClick();
                txtBarcodeScan.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Add || (e.Shift && e.KeyCode == Keys.Oemplus))
            {
                btnQtyPlus.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                btnQtyMinus.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtBarcodeScan_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtBarcodeScan.Text.Trim();
                if (!string.IsNullOrEmpty(barcode))
                {
                    ProcessBarcode(barcode);
                    txtBarcodeScan.Clear();
                    // Focus stays or is returned by ProcessBarcode logic
                }
                else if (cart.Count > 0)
                {
                    // Empty barcode Enter triggers checkout if items exist
                    btnCheckout.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessBarcode(string barcode)
        {
            try
            {
                var productRow = _saleService.GetProductByBarcode(barcode);
                if (productRow != null)
                {
                    // Select the product in the dropdown for visual feedback
                    cmbProducts.SelectedValue = Convert.ToInt32(productRow["ProductID"]);

                    // Move fast to next section: Quantity
                    numQuantity.Focus();
                    numQuantity.Select(0, numQuantity.Text.Length);
                }
                else
                {
                    MessageBox.Show("Product not found for barcode: " + barcode, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBarcodeScan.Focus();
                    txtBarcodeScan.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing barcode: " + ex.Message);
            }
        }

        private void DgvCart_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        private void POSForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                txtCustomerContact.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtBarcodeScan.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F12)
            {
                btnClearCart.PerformClick();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                txtDiscount.Focus();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                cmbCustomer.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnHold.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnResume.PerformClick();
                e.Handled = true;
            }
        }

        private void POSForm_Load(object? sender, EventArgs e)
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

        private void btnAddToCart_Click(object? sender, EventArgs e)
        {
            if (cmbProducts.SelectedValue == null || cmbProducts.SelectedValue == DBNull.Value) return;

            int productId = Convert.ToInt32(cmbProducts.SelectedValue);

            // Re-fetch product data from DataSource to ensure fresh stock levels
            if (cmbProducts.DataSource is not DataTable dt) return;
            DataRow[] rows = dt.Select($"ProductID = {productId}");
            if (rows.Length == 0) return;
            DataRow selectedProduct = rows[0];

            string productName = selectedProduct["ProductName"].ToString()!;
            decimal price = Convert.ToDecimal(selectedProduct["Price"]);
            int quantity = (int)numQuantity.Value;
            int stock = Convert.ToInt32(selectedProduct["StockQuantity"]);

            if (quantity > stock)
            {
                MessageBox.Show("Not enough stock available.", "Low Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingItem = cart.FirstOrDefault(i => i.ProductID == productId);
            if (existingItem != null)
            {
                if (existingItem.Quantity + quantity > stock)
                {
                    MessageBox.Show("Total quantity in cart exceeds available stock.", "Low Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            numQuantity.Value = 1;

            // Move fast back to Barcode for next scan
            txtBarcodeScan.Focus();
            txtBarcodeScan.Clear();
        }

        private void UpdateCartGrid()
        {
            dgvCart.DataSource = null;

            decimal runningTotal = 0;
            autoDiscount = 0;
            taxTotal = 0;

            // Calculate line item totals with auto-discounts and taxes
            foreach (var item in cart)
            {
                if (cmbProducts.DataSource is DataTable dt)
                {
                    var rows = dt.Select($"ProductID = {item.ProductID}");
                    if (rows.Length > 0)
                    {
                        decimal taxPercent = rows[0]["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rows[0]["TaxPercentage"]) : 0;
                        decimal permDiscPercent = rows[0]["DiscountRate"] != DBNull.Value ? Convert.ToDecimal(rows[0]["DiscountRate"]) : 0;
                        bool isBOGO = rows[0]["IsBOGO"] != DBNull.Value && Convert.ToBoolean(rows[0]["IsBOGO"]);
                        string? unitType = rows[0]["UnitType"]?.ToString();

                        decimal baseSubtotal = item.Quantity * item.UnitPrice;
                        decimal lineDiscount = baseSubtotal * (permDiscPercent / 100);

                        if (isBOGO && item.Quantity >= 2)
                        {
                            lineDiscount += (item.Quantity / 2) * item.UnitPrice;
                        }

                        decimal taxableAmount = baseSubtotal - lineDiscount;
                        decimal lineTax = taxableAmount * (taxPercent / 100);

                        item.Discount = lineDiscount;
                        item.Subtotal = taxableAmount + lineTax;

                        runningTotal += baseSubtotal;
                        autoDiscount += lineDiscount;
                        taxTotal += lineTax;
                    }
                }
            }

            dgvCart.DataSource = cart.Select(i => new {
                i.ProductName,
                i.Quantity,
                UnitPrice = i.UnitPrice.ToString("C"),
                Discount = i.Discount.ToString("C"),
                Subtotal = i.Subtotal.ToString("C")
            }).ToList();

            total = runningTotal;
            lblSubtotalValue.Text = total.ToString("C");

            decimal.TryParse(txtDiscount.Text, out manualDiscount);

            decimal finalTotal = (total - autoDiscount + taxTotal) - manualDiscount;
            if (finalTotal < 0) finalTotal = 0;

            lblTotalValue.Text = finalTotal.ToString("C");
        }

        private void txtDiscount_TextChanged(object? sender, EventArgs e)
        {
            UpdateCartGrid();
        }

        private void btnClearCart_Click(object? sender, EventArgs e)
        {
            if (cart.Count > 0 && MessageBox.Show("Are you sure you want to clear the cart?", "Clear Cart", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            cart.Clear();
            txtCustomerContact.Clear();
            txtBarcodeScan.Clear();
            UpdateCartGrid();
            txtCustomerContact.Focus();
        }

        private void txtCustomerContact_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string contact = txtCustomerContact.Text.Trim();
                if (!string.IsNullOrEmpty(contact))
                {
                    var dt = _customerService.GetCustomerByPhone(contact);
                    if (dt.Rows.Count > 0)
                    {
                        cmbCustomer.SelectedValue = dt.Rows[0]["CustomerID"];
                    }
                    else
                    {
                        MessageBox.Show("Customer not found with this contact number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                txtBarcodeScan.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnQtyPlus_Click(object? sender, EventArgs e)
        {
            numQuantity.Value = Math.Min(numQuantity.Maximum, numQuantity.Value + 1);
        }

        private void btnQtyMinus_Click(object? sender, EventArgs e)
        {
            numQuantity.Value = Math.Max(numQuantity.Minimum, numQuantity.Value - 1);
        }

        private void btnHold_Click(object? sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            heldCart = new List<SaleItem>(cart);
            if (cmbCustomer.SelectedValue != DBNull.Value && cmbCustomer.SelectedValue != null)
                heldCustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            else
                heldCustomerId = null;

            cart.Clear();
            UpdateCartGrid();
            btnResume.Enabled = true;
            MessageBox.Show("Sale held successfully.");
        }

        private void btnResume_Click(object? sender, EventArgs e)
        {
            if (heldCart == null) return;

            cart = new List<SaleItem>(heldCart);
            if (heldCustomerId != null)
                cmbCustomer.SelectedValue = heldCustomerId;
            else
                cmbCustomer.SelectedIndex = 0;

            heldCart = null;
            heldCustomerId = null;
            UpdateCartGrid();
            btnResume.Enabled = false;
        }

        private void btnCheckout_Click(object? sender, EventArgs e)
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
                    DiscountAmount = autoDiscount + manualDiscount,
                    TaxAmount = taxTotal,
                    FinalAmount = (total - autoDiscount + taxTotal) - manualDiscount > 0 ? (total - autoDiscount + taxTotal) - manualDiscount : 0,
                    Items = cart
                };

                // Open Receipt Preview
                using (var receipt = new ReceiptForm(sale))
                {
                    if (receipt.ShowDialog() == DialogResult.OK)
                    {
                        if (_saleService.ProcessSale(sale))
                        {
                            MessageBox.Show("Sale completed successfully!");
                            cart.Clear();
                            txtDiscount.Text = "0";
                            UpdateCartGrid();
                            LoadProducts();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during checkout: " + ex.Message);
            }
        }
    }
}
