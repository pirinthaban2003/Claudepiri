using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using POSApp.Data;
using POSApp.Models;
using POSApp.Utilities;

namespace POSApp.Services
{
    public class UserService
    {
        private readonly DatabaseHelper _dbHelper;

        public UserService()
        {
            _dbHelper = new DatabaseHelper();
        }

        public DataTable GetAllUsers()
        {
            string query = @"
                SELECT u.UserID, u.Username, r.RoleName, u.FullName, u.Email, u.IsActive, b.BranchName
                FROM Users u
                JOIN Roles r ON u.RoleID = r.RoleID
                LEFT JOIN Branches b ON u.BranchID = b.BranchID";
            return _dbHelper.ExecuteQuery(query);
        }

        public DataTable GetRoles()
        {
            return _dbHelper.ExecuteQuery("SELECT * FROM Roles");
        }

        public bool AddUser(string username, string password, int roleId, string fullName, string email, int? branchId)
        {
            string passwordHash = SecurityHelper.HashPassword(password);
            string query = "INSERT INTO Users (Username, PasswordHash, RoleID, FullName, Email, BranchID) VALUES (@username, @password, @roleId, @fullName, @email, @branchId)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@username", username),
                new MySqlParameter("@password", passwordHash),
                new MySqlParameter("@roleId", roleId),
                new MySqlParameter("@fullName", fullName),
                new MySqlParameter("@email", email),
                new MySqlParameter("@branchId", (object?)branchId ?? DBNull.Value)
            };
            return _dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateUserRole(int userId, int newRoleId)
        {
            string query = "UPDATE Users SET RoleID = @roleId WHERE UserID = @userId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@roleId", newRoleId),
                new MySqlParameter("@userId", userId)
            };
            return _dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool ToggleUserStatus(int userId, bool isActive)
        {
            string query = "UPDATE Users SET IsActive = @active WHERE UserID = @userId";
            MySqlParameter[] parameters = {
                new MySqlParameter("@active", isActive),
                new MySqlParameter("@userId", userId)
            };
            return _dbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
