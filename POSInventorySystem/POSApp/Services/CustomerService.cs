using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Services
{
    public class CustomerService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;

        public CustomerService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
        }

        public DataTable GetAllCustomers()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Customers");
        }

        public int SaveCustomer(Customer customer)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("@name", customer.CustomerName),
                new MySqlParameter("@phone", customer.Phone ?? (object)DBNull.Value),
                new MySqlParameter("@email", customer.Email ?? (object)DBNull.Value),
                new MySqlParameter("@points", customer.LoyaltyPoints),
                new MySqlParameter("@wallet", customer.WalletBalance),
                new MySqlParameter("@id", customer.CustomerID)
            };

            int result;
            if (customer.CustomerID == -1 || customer.CustomerID == 0)
            {
                string query = "INSERT INTO Customers (CustomerName, Phone, Email, LoyaltyPoints, WalletBalance) VALUES (@name, @phone, @email, @points, @wallet)";
                result = _dbHelper.ExecuteNonQuery(query, parameters);
                _auditService.LogAction($"Added Customer: {customer.CustomerName}", "Customer");
            }
            else
            {
                string query = "UPDATE Customers SET CustomerName=@name, Phone=@phone, Email=@email, LoyaltyPoints=@points, WalletBalance=@wallet WHERE CustomerID=@id";
                result = _dbHelper.ExecuteNonQuery(query, parameters);
                _auditService.LogAction($"Updated Customer: {customer.CustomerName}", "Customer");
            }
            return result;
        }

        public int DeleteCustomer(int customerId)
        {
            int result = _dbHelper.ExecuteNonQuery("DELETE FROM Customers WHERE CustomerID=@id", new MySqlParameter[] { new MySqlParameter("@id", customerId) });
            _auditService.LogAction($"Deleted Customer ID: {customerId}", "Customer");
            return result;
        }

        public void AddLoyaltyPoints(int customerId, int points)
        {
            _dbHelper.ExecuteNonQuery("UPDATE Customers SET LoyaltyPoints = LoyaltyPoints + @points WHERE CustomerID = @id",
                new MySqlParameter[] { new MySqlParameter("@points", points), new MySqlParameter("@id", customerId) });
        }
    }
}
