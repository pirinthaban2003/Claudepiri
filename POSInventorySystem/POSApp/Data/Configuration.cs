using Microsoft.Extensions.Configuration;
using System.IO;

namespace POSApp.Data
{
    public static class Configuration
    {
        private static IConfiguration? _configuration;

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        public static string Server => _configuration?["Database:Server"] ?? "localhost";
        public static string Database => _configuration?["Database:Name"] ?? "pos_db";
        public static string User => _configuration?["Database:User"] ?? "root";
        public static string Password => _configuration?["Database:Password"] ?? "password";

        public static string ConnectionString
        {
            get
            {
                var connStr = _configuration?.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connStr)) return connStr;
                return $"Server={Server};Database={Database};Uid={User};Pwd={Password};";
            }
        }
    }
}
