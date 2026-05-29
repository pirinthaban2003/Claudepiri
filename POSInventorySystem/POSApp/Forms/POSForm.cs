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
        private decimal redeemedDiscount = 0;
        private decimal walletDeduction = 0;
        private DataRow? currentCustomerRow = null;

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
            btnRedeemPoints.BackColor = ThemeHelper.AccentBlue;
            btnUseWallet.BackColor = ThemeHelper.AccentBlue;
            lblTotalValue.ForeColor = ThemeHelper.AccentGreen;

            // Remove global AcceptButton to allow context-sensitive Enter handling
            this.AcceptButton = null;
            dgvCart.CellFormatting += DgvCart_CellFormatting;

            btnResume.Enabled = heldCart != null;
            txtBarcodeScan.KeyDown += TxtBarcodeScan_KeyDown;
            numQuantity.KeyDown += NumQuantity_KeyDown;
            cmbProducts.KeyDown += CmbProducts_KeyDown;

            // Auto-select text on focus for speed
            txtCustomerContact.GotFocus += (s, e) => txtCustomerContact.SelectAll();
            txtBarcodeScan.GotFocus += (s, e) => txtBarcodeScan.SelectAll();
            txtProductSearch.GotFocus += (s, e) => txtProductSearch.SelectAll();
            numQuantity.GotFocus += (s, e) => numQuantity.Select(0, numQuantity.Text.Length);

            txtCustomerContact.TextChanged += TxtCustomerContact_TextChanged;
            txtBarcodeScan.TextChanged += TxtBarcodeScan_TextChanged;
            txtProductSearch.TextChanged += TxtProductSearch_TextChanged;
            cmbCustomer.SelectedIndexChanged += CmbCustomer_SelectedIndexChanged;

            btnRedeemPoints.Enabled = false;
            btnUseWallet.Enabled = false;
        }

        private void CmbCustomer_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateCustomerDisplay();
        }

        private void UpdateCustomerDisplay()
        {
            currentCustomerRow = null;
            lblPointsValue.Text = "0";
            lblWalletValue.Text = "Rs. 0.00";
            btnRedeemPoints.Enabled = false;
            btnUseWallet.Enabled = false;
            redeemedDiscount = 0;
            walletDeduction = 0;

            if (cmbCustomer.SelectedValue != null && cmbCustomer.SelectedValue != DBNull.Value)
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                var dt = _customerService.GetCustomerByID(customerId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    currentCustomerRow = dt.Rows[0];
                    int points = Convert.ToInt32(currentCustomerRow["LoyaltyPoints"]);
                    decimal wallet = Convert.ToDecimal(currentCustomerRow["WalletBalance"]);

                    lblPointsValue.Text = points.ToString();
                    lblWalletValue.Text = "Rs. " + wallet.ToString("N2");

                    btnRedeemPoints.Enabled = points > 0;
                    btnUseWallet.Enabled = wallet > 0;
                }
            }
            UpdateCartGrid();
        }

        private void TxtProductSearch_TextChanged(object? sender, EventArgs e)
        {
            string search = txtProductSearch.Text.Trim();
            if (search.Length >= 2)
            {
                var dt = _saleService.SearchProducts(search);

                // Safe binding pattern to prevent ArgumentOutOfRangeException
                cmbProducts.SelectedIndex = -1;
                cmbProducts.DisplayMember = "ProductName";
                cmbProducts.ValueMember = "ProductID";
                cmbProducts.DataSource = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbProducts.DroppedDown = true;
                }
            }
            else
            {
                cmbProducts.SelectedIndex = -1;
                cmbProducts.DataSource = null;
            }
        }

        private void CmbProducts_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddToCart.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtProductSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbProducts.Items.Count > 0)
                {
                    if (cmbProducts.SelectedIndex == -1) cmbProducts.SelectedIndex = 0;
                    btnAddToCart.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && cmbProducts.Items.Count > 0)
            {
                cmbProducts.Focus();
                e.Handled = true;
            }
        }

        private void TxtBarcodeScan_TextChanged(object? sender, EventArgs e)
        {
            string barcode = txtBarcodeScan.Text.Trim();
            if (barcode.Length >= 3) // Optimize: only search if at least 3 chars
            {
                // Try to find a match without showing error messages (silent mode)
                ProcessBarcode(barcode, true);
            }
        }

        private void TxtCustomerContact_TextChanged(object? sender, EventArgs e)
        {
            string contact = txtCustomerContact.Text.Trim();
            if (contact.Length >= 10)
            {
                var dt = _customerService.GetCustomerByPhone(contact);
                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbCustomer.SelectedValue = dt.Rows[0]["CustomerID"];
                    txtBarcodeScan.Focus();
                }
            }
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
                    ProcessBarcode(barcode, false); // Explicit Enter should show error if not found
                    txtBarcodeScan.Clear();
                }
                else if (cmbProducts.SelectedValue != null && cmbProducts.SelectedValue != DBNull.Value)
                {
                    // If barcode is empty but a product is selected, add it to cart
                    btnAddToCart.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ProcessBarcode(string barcode, bool silent)
        {
            try
            {
                var productRow = _saleService.GetProductByBarcode(barcode);
                if (productRow != null)
                {
                    // Exact match found!
                    if (!silent)
                    {
                        // Explicit search (Enter key): Add to cart immediately
                        AddRowToCart(productRow);
                        txtBarcodeScan.Clear();
                    }
                    else
                    {
                        // Silent search (typing): Select for feedback and move focus
                        cmbProducts.SelectedValue = Convert.ToInt32(productRow["ProductID"]);
                        numQuantity.Focus();
                        numQuantity.Select(0, numQuantity.Text.Length);
                    }
                }
                else if (!silent)
                {
                    MessageBox.Show("Product not found for barcode: " + barcode, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBarcodeScan.Focus();
                    txtBarcodeScan.SelectAll();
                }
            }
            catch (Exception ex)
            {
                if (!silent) MessageBox.Show("Error processing barcode: " + ex.Message);
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
            else if (e.KeyCode == Keys.F3)
            {
                txtProductSearch.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnCheckout.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F12)
            {
                btnClearCart.PerformClick();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Add || (e.Shift && e.KeyCode == Keys.Oemplus))
            {
                if (!txtCustomerContact.Focused && !txtBarcodeScan.Focused && !txtDiscount.Focused)
                {
                    btnQtyPlus.PerformClick();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                if (!txtCustomerContact.Focused && !txtBarcodeScan.Focused && !txtDiscount.Focused)
                {
                    btnQtyMinus.PerformClick();
                    e.Handled = true;
                }
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
            // Initial load is now empty to support high-speed on-demand search
            cmbProducts.DataSource = null;
        }

        private void LoadCustomers()
        {
            try
            {
                var dt = _customerService.GetAllCustomers();
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["CustomerID"] = DBNull.Value;
                    dr["CustomerName"] = "Walk-in Customer";
                    dt.Rows.InsertAt(dr, 0);

                    cmbCustomer.DisplayMember = "CustomerName";
                    cmbCustomer.ValueMember = "CustomerID";
                    cmbCustomer.DataSource = dt;
                }
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

            // Re-fetch product data from DataSource if available
            if (cmbProducts.DataSource is DataTable dt)
            {
                DataRow[] rows = dt.Select($"ProductID = {productId}");
                if (rows.Length > 0)
                {
                    AddRowToCart(rows[0]);
                    return;
                }
            }

            // If not in DataSource, fetch fresh from DB
            var productRow = _saleService.GetProductByID(productId);
            if (productRow != null)
            {
                AddRowToCart(productRow);
            }
        }

        private void AddRowToCart(DataRow selectedProduct)
        {
            int productId = Convert.ToInt32(selectedProduct["ProductID"]);
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
                    Subtotal = quantity * price,
                    TaxPercentage = selectedProduct.Table.Columns.Contains("TaxPercentage") && selectedProduct["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(selectedProduct["TaxPercentage"]) : 0,
                    DiscountRate = selectedProduct.Table.Columns.Contains("DiscountRate") && selectedProduct["DiscountRate"] != DBNull.Value ? Convert.ToDecimal(selectedProduct["DiscountRate"]) : 0,
                    IsBOGO = selectedProduct.Table.Columns.Contains("IsBOGO") && selectedProduct["IsBOGO"] != DBNull.Value && Convert.ToBoolean(selectedProduct["IsBOGO"])
                });
            }

            UpdateCartGrid();
            numQuantity.Value = 1;

            // Clear search after adding
            txtProductSearch.Clear();

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

            // Calculate line item totals with auto-discounts and taxes using persistent item metadata
            foreach (var item in cart)
            {
                decimal taxPercent = item.TaxPercentage;
                decimal permDiscPercent = item.DiscountRate;
                bool isBOGO = item.IsBOGO;

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

            dgvCart.DataSource = cart.Select(i => new {
                i.ProductName,
                i.Quantity,
                UnitPrice = "Rs. " + i.UnitPrice.ToString("N2"),
                Discount = "Rs. " + i.Discount.ToString("N2"),
                Subtotal = "Rs. " + i.Subtotal.ToString("N2")
            }).ToList();

            total = runningTotal;
            lblSubtotalValue.Text = "Rs. " + total.ToString("N2");

            decimal.TryParse(txtDiscount.Text, out manualDiscount);

            decimal finalTotal = (total - autoDiscount + taxTotal) - manualDiscount - redeemedDiscount - walletDeduction;
            if (finalTotal < 0) finalTotal = 0;

            lblTotalValue.Text = "Rs. " + finalTotal.ToString("N2");
        }

        private void btnRedeemPoints_Click(object sender, EventArgs e)
        {
            if (currentCustomerRow == null) return;

            int points = Convert.ToInt32(currentCustomerRow["LoyaltyPoints"]);
            decimal finalTotalBeforeRedeem = (total - autoDiscount + taxTotal) - manualDiscount - redeemedDiscount - walletDeduction;

            if (finalTotalBeforeRedeem <= 0) return;

            // Simple rule: 1 point = 1 Rupee
            decimal maxRedeemable = Math.Min((decimal)points, finalTotalBeforeRedeem);

            redeemedDiscount += maxRedeemable;
            UpdateCartGrid();
            MessageBox.Show($"Redeemed {maxRedeemable:N0} points (Rs. {maxRedeemable:N2} discount).");
        }

        private void btnUseWallet_Click(object sender, EventArgs e)
        {
            if (currentCustomerRow == null) return;

            decimal wallet = Convert.ToDecimal(currentCustomerRow["WalletBalance"]);
            decimal finalTotalBeforeWallet = (total - autoDiscount + taxTotal) - manualDiscount - redeemedDiscount - walletDeduction;

            if (finalTotalBeforeWallet <= 0) return;

            decimal maxDeductible = Math.Min(wallet, finalTotalBeforeWallet);

            walletDeduction += maxDeductible;
            UpdateCartGrid();
            MessageBox.Show($"Using Rs. {maxDeductible:N2} from wallet.");
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
            redeemedDiscount = 0;
            walletDeduction = 0;
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
                    if (dt != null && dt.Rows.Count > 0)
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

        private void btnBack_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResume_Click(object? sender, EventArgs e)
        {
            if (heldCart == null) return;

            cart = new List<SaleItem>(heldCart);
            if (heldCustomerId != null)
                cmbCustomer.SelectedValue = heldCustomerId;
            else if (cmbCustomer.Items.Count > 0)
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

                decimal finalAmount = (total - autoDiscount + taxTotal) - manualDiscount - redeemedDiscount - walletDeduction;
                if (finalAmount < 0) finalAmount = 0;

                Sale sale = new Sale
                {
                    CustomerID = customerId,
                    TotalAmount = total,
                    DiscountAmount = autoDiscount + manualDiscount + redeemedDiscount,
                    TaxAmount = taxTotal,
                    FinalAmount = finalAmount,
                    RedeemedPoints = redeemedDiscount,
                    WalletDeduction = walletDeduction,
                    Items = new List<SaleItem>(cart)
                };

                // Process Sale First to get ID
                int saleId = _saleService.ProcessSale(sale);
                if (saleId > 0)
                {
                    // Open Receipt Preview with persistent SaleID
                    using (var receipt = new ReceiptForm(sale))
                    {
                        receipt.ShowDialog();

                        MessageBox.Show($"Sale #{saleId} completed successfully!");
                        cart.Clear();
                        txtDiscount.Text = "0";
                        UpdateCartGrid();
                        LoadProducts();
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
