namespace POSApp.Data
{
    public static class Configuration
    {
        public static string Server { get; set; } = "localhost";
        public static string Database { get; set; } = "pos_db";
        public static string User { get; set; } = "root";
        public static string Password { get; set; } = "password";

        public static string ConnectionString => $"Server={Server};Database={Database};Uid={User};Pwd={Password};";
    }
}
