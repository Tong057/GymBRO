using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
