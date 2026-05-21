using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Services
{
    public class SupplierService
    {
        private readonly DatabaseHelper _dbHelper;

        public SupplierService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public DataTable GetAllSuppliers()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Suppliers");
        }

        public int SaveSupplier(Supplier supplier)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("@name", supplier.SupplierName),
                new MySqlParameter("@contact", (object?)supplier.ContactPerson ?? DBNull.Value),
                new MySqlParameter("@phone", (object?)supplier.Phone ?? DBNull.Value),
                new MySqlParameter("@email", (object?)supplier.Email ?? DBNull.Value),
                new MySqlParameter("@address", (object?)supplier.Address ?? DBNull.Value),
                new MySqlParameter("@id", supplier.SupplierID)
            };

            if (supplier.SupplierID == -1 || supplier.SupplierID == 0)
            {
                string query = "INSERT INTO Suppliers (SupplierName, ContactPerson, Phone, Email, Address) VALUES (@name, @contact, @phone, @email, @address)";
                return _dbHelper.ExecuteNonQuery(query, parameters);
            }
            else
            {
                string query = "UPDATE Suppliers SET SupplierName=@name, ContactPerson=@contact, Phone=@phone, Email=@email, Address=@address WHERE SupplierID=@id";
                return _dbHelper.ExecuteNonQuery(query, parameters);
            }
        }

        public int DeleteSupplier(int supplierId)
        {
            return _dbHelper.ExecuteNonQuery("DELETE FROM Suppliers WHERE SupplierID=@id", new MySqlParameter[] { new MySqlParameter("@id", supplierId) });
        }
    }
}
