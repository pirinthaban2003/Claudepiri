using POSApp.Forms;
using POSApp.Services;

namespace POSApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        // Run migrations
        new DatabaseMigration().EnsureSchemaUpToDate();

        using (LoginForm loginForm = new LoginForm())
        {
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new DashboardForm());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}