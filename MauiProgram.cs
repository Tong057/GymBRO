using AiForms.Settings;
using CommunityToolkit.Maui;
using GymBro.Models.Data.EntityFramework;
using GymBro.Models.Data.EntityFramework.DbProviders;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.ViewModels;
using GymBro.Views;
using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;
using GymBro.Views.Popups.Exercise;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace GymBro
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterServices()
                .UseSkiaSharp(true)
                .UseBottomSheet()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddSettingsViewHandler();
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome-Brands.otf", "FontAwesomeBrands");
                    fonts.AddFont("FontAwesome-Solid.otf", "FontAwesomeSolid");
                    fonts.AddFont("BebasNeue-Regular.ttf", "BebasRegular");
                    fonts.AddFont("BebasNeue-Bold.ttf", "BebasBold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            //EntityFramework
            builder.Services.AddTransient<ApplicationContext>();
            builder.Services.AddTransient<IProvider, Provider>();
            builder.Services.AddSingleton<Repository>();

            return builder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<TrainingDaysViewModel>();
            builder.Services.AddTransient<TrainingPlanViewModel>();
            builder.Services.AddTransient<TrainingDayViewModel>();
            builder.Services.AddTransient<StatisticsViewModel>();

            return builder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<TrainingDaysPage>();
            builder.Services.AddTransient<CreateTrainingPlanPage>();
            builder.Services.AddTransient<EditTrainingPlanPage>();
            builder.Services.AddTransient<CreateExercisePopup>();
            builder.Services.AddTransient<EditExercisePopup>();
            builder.Services.AddTransient<TrainingDayPage>();
            builder.Services.AddTransient<StatisticsPage>();

            return builder;
        }
    }
}
