using C971.Data;

namespace C971
{
    public partial class App : Application
    {
        // Single shared database instance
        public static AppDatabase Database { get; } = new();

        public App()
        {
            InitializeComponent();

            // Ensure tables exist
            _ = Database.InitAsync();

            MainPage = new AppShell();
        }
    }
}
