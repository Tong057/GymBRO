using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Utilities;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Alerts;
using GymBro.Models.Data.EntityFramework;

namespace GymBro.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private Repository _repository;
        public SettingsViewModel(Repository repository)
        {
            _repository = repository;
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

        [RelayCommand]
        private void DropDatabase()
        {
            _repository.ClearAllData();
        }
    }
}
