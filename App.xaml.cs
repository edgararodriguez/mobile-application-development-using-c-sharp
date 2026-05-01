using C971.Data;
using C971.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace C971
{
    public partial class App : Application
    {
        public static AppDatabase Database =>
            Current?.Handler?.MauiContext?.Services.GetRequiredService<AppDatabase>()
            ?? throw new InvalidOperationException("AppDatabase is not available.");

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
