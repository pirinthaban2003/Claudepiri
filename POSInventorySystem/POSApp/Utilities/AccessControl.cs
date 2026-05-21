using System.Collections.Generic;

namespace POSApp.Utilities
{
    public static class AccessControl
    {
        private static readonly Dictionary<string, List<string>> RolePermissions = new Dictionary<string, List<string>>
        {
            { "Admin", new List<string> { "POS", "Inventory", "Suppliers", "Customers", "Reports", "Expenses", "Returns" } },
            { "Manager", new List<string> { "Inventory", "Suppliers", "Reports", "Expenses" } },
            { "Cashier", new List<string> { "POS", "Returns" } },
            { "Inventory Staff", new List<string> { "Inventory", "Suppliers" } }
        };

        public static bool CanAccess(string roleName, string moduleName)
        {
            if (string.IsNullOrEmpty(roleName) || !RolePermissions.ContainsKey(roleName))
                return false;

            return RolePermissions[roleName].Contains(moduleName);
        }
    }
}
