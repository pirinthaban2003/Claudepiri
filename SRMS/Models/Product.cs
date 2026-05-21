namespace SRMS.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Barcode { get; set; }
        public string? SKU { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Unit { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal TaxRate { get; set; }
        public string Status { get; set; } = "Active";
        public string? ImagePath { get; set; }
    }
}
