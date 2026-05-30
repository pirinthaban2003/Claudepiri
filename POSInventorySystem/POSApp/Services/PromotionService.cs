using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Services
{
    public class PromotionService
    {
        private readonly DatabaseHelper _dbHelper;

        public PromotionService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public DataTable GetActiveOffers()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Offers WHERE IsActive = 1 AND CURDATE() BETWEEN StartDate AND EndDate");
        }

        public decimal CalculateDiscount(List<SaleItem> cart, int? customerId)
        {
            decimal totalDiscount = 0;
            var activeOffers = GetActiveOffers();

            foreach (DataRow offer in activeOffers.Rows)
            {
                string type = offer["OfferType"].ToString()!;
                decimal value = Convert.ToDecimal(offer["Value"]);

                if (type == "PERCENTAGE")
                {
                    decimal cartTotal = cart.Sum(i => i.Subtotal);
                    totalDiscount += cartTotal * (value / 100);
                }
                else if (type == "FIXED")
                {
                    totalDiscount += value;
                }
                // More complex logic for BOGO/Combo can be added here
            }

            return totalDiscount;
        }
    }
}
