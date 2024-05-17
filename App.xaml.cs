using GymBro.Models.Entities;
using GymBro.Models.Entities.Statistics;
using GymBro.Utilities;
using LiveChartsCore;

namespace GymBro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            ApplicationSettings.SetTheme(Preferences.Default.Get<string?>("app_theme", null));
            ApplicationSettings.SetLanguage(Preferences.Default.Get<string?>("app_language", null));
        }

        protected override void OnStart()
        {
            ApplicationSettings.SetTheme(Preferences.Default.Get<string?>("app_theme", null));
            ApplicationSettings.SetLanguage(Preferences.Default.Get<string?>("app_language", null));

            base.OnStart();

            LiveCharts.Configure(config =>
                config
                    // you can override the theme 
                    //.AddDarkTheme()  

                    // In case you need a non-Latin based font, you must register a typeface for SkiaSharp
                    //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')) // <- Chinese 
                    //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('あ')) // <- Japanese 
                    //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('헬')) // <- Korean 
                    //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('Ж'))  // <- Russian 

                    //.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('أ'))  // <- Arabic 
                    //.UseRightToLeftSettings() // Enables right to left tooltips 

                    // finally register your own mappers
                    // you can learn more about mappers at:
                    // https://livecharts.dev/docs/maui/2.0.0-rc2/Overview.Mappers

                    // here we use the index as X, and the population as Y 
                    .HasMap<StatisticsExerciseStatus>((exStat, data) => new(data, exStat.WeightedAverageWeight))
            // .HasMap<Foo>( .... ) 
            // .HasMap<Bar>( .... ) 
            );
        }
    }
}
