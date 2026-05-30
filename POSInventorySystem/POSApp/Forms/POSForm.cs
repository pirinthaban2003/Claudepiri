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
        private readonly InventoryService _inventoryService;
        private List<SaleItem> cart = new List<SaleItem>();
        private decimal exchangeRate = 90000; // Default LBP/USD rate
        private int? selectedCategoryId = null;
        private int? currentCustomerId = null;

        public POSForm()
        {
            InitializeComponent();
            _saleService = new SaleService();
            _customerService = new CustomerService();
            _inventoryService = new InventoryService();
            this.KeyPreview = true;
            this.KeyDown += POSForm_KeyDown;
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnBack.Click += (s, e) => this.Close();
            txtBarcode.KeyDown += TxtBarcode_KeyDown;
            txtCustomerPhone.KeyDown += TxtCustomerPhone_KeyDown;
            btnCheckout.Click += BtnCheckout_Click;
            btnClearCart.Click += BtnClearCart_Click;
        }

        private void POSForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadProducts();
            UpdateCartGrid();
            txtBarcode.Focus();
        }

        private void LoadCategories()
        {
            try
            {
                flpCategories.Controls.Clear();
                var dt = _inventoryService.GetCategories();

                // Add "All" Category
                AddCategoryButton(null, "ALL");

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        AddCategoryButton(Convert.ToInt32(row["CategoryID"]), row["CategoryName"]?.ToString() ?? "N/A");
                    }
                }
            }
            catch { }
        }

        private void AddCategoryButton(int? id, string name)
        {
            Button btn = new Button
            {
                Text = name,
                Size = new Size(120, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = (selectedCategoryId == id) ? ThemeHelper.AccentBlue : Color.White,
                ForeColor = (selectedCategoryId == id) ? Color.White : ThemeHelper.AccentBlue,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Tag = id,
                Margin = new Padding(3)
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = ThemeHelper.AccentBlue;
            btn.Click += (s, e) => {
                selectedCategoryId = (int?)btn.Tag;
                LoadProducts();
                foreach (Control ctrl in flpCategories.Controls)
                {
                    if (ctrl is Button b)
                    {
                        b.BackColor = (selectedCategoryId == (int?)b.Tag) ? ThemeHelper.AccentBlue : Color.White;
                        b.ForeColor = (selectedCategoryId == (int?)b.Tag) ? Color.White : ThemeHelper.AccentBlue;
                    }
                }
            };
            flpCategories.Controls.Add(btn);
        }

        private void LoadProducts()
        {
            try
            {
                flpProducts.Controls.Clear();
                var dt = _saleService.GetAvailableProducts();

                IEnumerable<DataRow> rows = dt.AsEnumerable();
                if (selectedCategoryId != null)
                {
                    rows = rows.Where(r => r.Table.Columns.Contains("CategoryID") && r["CategoryID"] != DBNull.Value && Convert.ToInt32(r["CategoryID"]) == selectedCategoryId);
                }

                foreach (DataRow row in rows)
                {
                    AddProductTile(row);
                }
            }
            catch { }
        }

        private void AddProductTile(DataRow row)
        {
            int id = Convert.ToInt32(row["ProductID"]);
            string name = row["ProductName"]?.ToString() ?? "Unknown";
            decimal price = Convert.ToDecimal(row["Price"]);
            int stock = Convert.ToInt32(row["StockQuantity"]);

            Panel pnl = new Panel { Size = new Size(160, 180), BackColor = Color.White, Margin = new Padding(5), BorderStyle = BorderStyle.FixedSingle };

            Label lblName = new Label {
                Text = name,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label lblPrice = new Label {
                Text = $"$ {price:N2}\n{price * exchangeRate:N0} LBP",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = ThemeHelper.AccentBlue,
                Font = new Font("Segoe UI", 9)
            };

            Label lblStock = new Label {
                Text = $"Stock: {stock}",
                Dock = DockStyle.Bottom,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 8)
            };

            pnl.Controls.Add(lblPrice);
            pnl.Controls.Add(lblName);
            pnl.Controls.Add(lblStock);

            pnl.Cursor = Cursors.Hand;
            foreach (Control c in pnl.Controls) c.Click += (s, e) => AddToCart(row);
            pnl.Click += (s, e) => AddToCart(row);

            flpProducts.Controls.Add(pnl);
        }

        private void AddToCart(DataRow product)
        {
            int id = Convert.ToInt32(product["ProductID"]);
            var existing = cart.FirstOrDefault(i => i.ProductID == id);

            if (existing != null)
            {
                existing.Quantity++;
                existing.Subtotal = existing.Quantity * existing.UnitPrice;
            }
            else
            {
                cart.Add(new SaleItem
                {
                    ProductID = id,
                    ProductName = product["ProductName"]?.ToString() ?? "Unknown",
                    Quantity = 1,
                    UnitPrice = Convert.ToDecimal(product["Price"]),
                    Subtotal = Convert.ToDecimal(product["Price"]),
                    TaxPercentage = product.Table.Columns.Contains("TaxPercentage") && product["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(product["TaxPercentage"]) : 0
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
                Price = i.UnitPrice.ToString("N2"),
                Total = i.Subtotal.ToString("N2")
            }).ToList();

            decimal totalUSD = cart.Sum(i => i.Subtotal);
            decimal totalLBP = totalUSD * exchangeRate;

            lblTotalUSD.Text = $"$ {totalUSD:N2}";
            lblTotalLBP.Text = $"{totalLBP:N0} LBP";
        }

        private void TxtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = txtBarcode.Text.Trim();
                if (!string.IsNullOrEmpty(barcode))
                {
                    var product = _saleService.GetProductByBarcode(barcode);
                    if (product != null)
                    {
                        AddToCart(product);
                        txtBarcode.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Product not found!");
                    }
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtCustomerPhone_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string phone = txtCustomerPhone.Text.Trim();
                var dt = _customerService.GetCustomerByPhone(phone);
                if (dt != null && dt.Rows.Count > 0)
                {
                    currentCustomerId = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                    lblCustomerName.Text = dt.Rows[0]["CustomerName"]?.ToString() ?? "N/A";
                    lblCustomerName.ForeColor = ThemeHelper.AccentBlue;
                    txtBarcode.Focus();
                }
                else
                {
                    MessageBox.Show("Customer not found.");
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void POSForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1) txtCustomerPhone.Focus();
            if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.F12) txtBarcode.Focus();
            if (e.KeyCode == Keys.F10) BtnCheckout_Click(this, EventArgs.Empty);
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            if (cart.Count == 0) return;

            try
            {
                decimal totalUSD = cart.Sum(i => i.Subtotal);
                decimal finalAmountLBP = totalUSD * exchangeRate;

                Sale sale = new Sale
                {
                    CustomerID = currentCustomerId,
                    TotalAmount = totalUSD * exchangeRate, // Using LBP for base field compatibility if needed
                    TotalAmountUSD = totalUSD,
                    ExchangeRate = exchangeRate,
                    FinalAmount = finalAmountLBP,
                    FinalAmountUSD = totalUSD,
                    Items = new List<SaleItem>(cart)
                };

                int saleId = _saleService.ProcessSale(sale);
                if (saleId > 0)
                {
                    using (var receipt = new ReceiptForm(sale))
                    {
                        receipt.ShowDialog();
                    }

                    MessageBox.Show($"Sale #{saleId} completed successfully!");
                    cart.Clear();
                    UpdateCartGrid();
                    txtCustomerPhone.Clear();
                    lblCustomerName.Text = "Walk-in Guest...";
                    lblCustomerName.ForeColor = Color.Black;
                    currentCustomerId = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during checkout: " + ex.Message);
            }
        }

        private void BtnClearCart_Click(object? sender, EventArgs e)
        {
            if (cart.Count > 0 && MessageBox.Show("Clear cart?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                cart.Clear();
                UpdateCartGrid();
            }
        }
    }
}
