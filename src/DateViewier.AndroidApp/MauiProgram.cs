using DateViewier.AndroidApp.Repositories;
using DateViewier.AndroidApp.Services;
using DateViewier.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace DateViewier.AndroidApp
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddTransient<IInstanceRepistory<WorkCalendar>, WorkCalendarPreferencesRepository>();
            builder.Services.AddTransient<IInstanceRepistory<WorkweekInfo>, WorkweekInfoPreferencesRepository>();
            builder.Services.AddTransient<IOnlyLoadInstanceRepositoy<WorkdayRescheduleChecker>, WorkdayRescheduleCheckerMemoryRepository>();

            builder.Services.AddTransient<WorkCalendarService>();
            builder.Services.AddTransient<WorkweekInfoService>();
            builder.Services.AddTransient<HolidayService>();

            builder.Services.AddLocalization();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            SetCultureInfo();

            var app = builder.Build();
            app.SetTwoTwoWorkweek();
            return app;
        }

        private static void SetCultureInfo()
        {
            CultureInfo info = new CultureInfo("ru-Ru");
            CultureInfo.DefaultThreadCurrentCulture = info;
            CultureInfo.DefaultThreadCurrentUICulture = info;
        }
    }
}
