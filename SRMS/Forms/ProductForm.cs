using SRMS.Models;
using SRMS.Repositories;
using SRMS.Utilities;

namespace SRMS.Forms
{
    public partial class ProductForm : Form
    {
        private readonly ProductRepository _productRepository;

        public ProductForm()
        {
            _productRepository = new ProductRepository();
            InitializeComponent();
            UIStyle.ApplyStyle(this);
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            dgvProducts.DataSource = products;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                ProductName = txtName.Text,
                SellingPrice = decimal.TryParse(txtPrice.Text, out decimal price) ? price : 0,
                Status = "Active"
            };

            if (await _productRepository.AddProductAsync(product))
            {
                MessageBox.Show("Product added successfully!");
                LoadProducts();
            }
        }
    }
}
