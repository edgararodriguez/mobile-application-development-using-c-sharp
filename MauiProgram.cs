using Microsoft.Extensions.Logging;
using C971.Data;
using C971.Repositories;
using C971.ViewModels.Terms;
using C971.Pages.Terms;

namespace C971
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Initialize SQLite
            SQLitePCL.Batteries_V2.Init();

            // Register app services and repositories (Dependency Injection)
            builder.Services.AddSingleton<AppDatabase>();
            builder.Services.AddSingleton<ITermRepository, TermRepository>();
            builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
            builder.Services.AddSingleton<IAssessmentRepository, AssessmentRepository>();
            // Register ViewModels and Pages
            builder.Services.AddTransient<TermsListViewModel>();
            builder.Services.AddTransient<TermsListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
