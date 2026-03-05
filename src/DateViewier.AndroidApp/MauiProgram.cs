using DateViewier.AndroidApp.Repositories;
using DateViewier.AndroidApp.Services;
using DateViewier.Models;
using Microsoft.Extensions.Logging;

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
            builder.Services.AddTransient<WorkCalendarService>();
            builder.Services.AddTransient<WorkweekInfoService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            app.SetTwoTwoWorkweek();
            return app;
        }
    }
}
