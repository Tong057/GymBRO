using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Utilities;

namespace GymBro.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        public SettingsViewModel()
        {
            SelectedLanguage = ApplicationSettings.CurrentLanguage;
            SelectedTheme = ApplicationSettings.CurrentTheme;
        }

        [ObservableProperty]
        private string? _selectedLanguage;

        partial void OnSelectedLanguageChanged(string? value)
        {
            ApplicationSettings.SetLanguage(value);
        }

        [ObservableProperty]
        private string? _selectedTheme;

        partial void OnSelectedThemeChanged(string? value)
        {
            ApplicationSettings.SetTheme(value);
        }
    }
}
