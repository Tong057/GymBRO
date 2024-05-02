using AiForms.Settings;
using GymBro.Models.Data.EntityFramework;
using GymBro.Models.Data.EntityFramework.DbProviders;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.ViewModels;
using GymBro.Views;
using Microsoft.Extensions.Logging;

namespace GymBro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    if (DeviceInfo.Platform != DevicePlatform.WinUI)
                        handlers.AddSettingsViewHandler();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //Pages
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsPage>();

            builder.Services.AddTransient<TrainingDaysViewModel>();
            builder.Services.AddTransient<TrainingDaysPage>();

            //EntityFramework
            builder.Services.AddTransient<ApplicationContext>();
            builder.Services.AddTransient<Provider>();
            builder.Services.AddTransient<Repository>();

            return builder.Build();

        }
    }
}
