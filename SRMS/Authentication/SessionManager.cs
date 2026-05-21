using SRMS.Models;

namespace SRMS.Authentication
{
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }
        public static DateTime? LoginTime { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = user;
            LoginTime = DateTime.Now;
        }

        public static void Logout()
        {
            CurrentUser = null;
            LoginTime = null;
        }

        public static bool IsLoggedIn() => CurrentUser != null;
    }
}
