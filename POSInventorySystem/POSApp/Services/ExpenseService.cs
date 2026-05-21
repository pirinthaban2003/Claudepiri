using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;

namespace POSApp.Services
{
    public class ExpenseService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AuditService _auditService;

        public ExpenseService()
        {
            _dbHelper = new DatabaseHelper();
            _auditService = new AuditService();
        }

        public DataTable GetAllExpenses()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Expenses ORDER BY ExpenseDate DESC");
        }

        public int AddExpense(string title, string category, decimal amount, DateTime date, string description)
        {
            string query = "INSERT INTO Expenses (ExpenseTitle, Category, Amount, ExpenseDate, Description) VALUES (@title, @cat, @amount, @date, @desc)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@title", title),
                new MySqlParameter("@cat", category),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@date", date),
                new MySqlParameter("@desc", description)
            };
            int result = _dbHelper.ExecuteNonQuery(query, parameters);
            _auditService.LogAction($"Added Expense: {title}, Amount: {amount}", "Expenses");
            return result;
        }

        public decimal GetTotalExpenses(DateTime start, DateTime end)
        {
            string query = "SELECT SUM(Amount) FROM Expenses WHERE ExpenseDate BETWEEN @start AND @end";
            var result = _dbHelper.ExecuteScalar(query, new MySqlParameter[] {
                new MySqlParameter("@start", start),
                new MySqlParameter("@end", end)
            });
            return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
    }
}
