using GymBro.Resources.Languages;
using GymBro.Resources.Themes;
using System.Globalization;

namespace GymBro.Models
{
    public static class ApplicationSettings
    {
        public static string CurrentTheme { get; set; }
        public static void SetTheme(string? appTheme)
        {
            Application.Current.Resources.Clear();
            Preferences.Default.Set("app_theme", appTheme);
            switch (appTheme)
            {
                case "Dark":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
                    CurrentTheme = "Dark";
                    break;

                case "Light":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
                    CurrentTheme = "Light";
                    break;

                case null:
                    if (Application.Current.PlatformAppTheme == AppTheme.Light)
                    {
                        goto case "Light";
                    }
                    else
                    {
                        goto case "Dark";
                    }
            }
        }

        public static string CurrentLanguage { get; set; }
        public static void SetLanguage(string? appLanguage)
        {
            Application.Current.Resources.Clear();
            Preferences.Default.Set("app_language", appLanguage);
            switch (appLanguage)
            {
                case "English":
                    Application.Current.Resources.MergedDictionaries.Add(new EnglishLanguage());
                    CurrentLanguage = "English";
                    break;

                case "Russian":
                    Application.Current.Resources.MergedDictionaries.Add(new RussianLanguage());
                    CurrentLanguage = "Russian";
                    break;

                case "Polish":
                    Application.Current.Resources.MergedDictionaries.Add(new PolishLanguage());
                    CurrentLanguage = "Polish";
                    break;

                case null:
                    var currentCulture = CultureInfo.CurrentCulture;
                    var sysLang = currentCulture.TwoLetterISOLanguageName;

                    if (sysLang == "ru")
                    {
                        goto case "Russian";
                    }
                    else if (sysLang == "pl")
                    {
                        goto case "Polish";
                    }
                    else
                    {
                        goto case "English";
                    }
            }

        }
    }
}
