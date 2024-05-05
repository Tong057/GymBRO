using GymBro.Models;

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
    }
}
