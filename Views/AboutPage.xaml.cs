using Microsoft.Maui.Controls;

namespace GymBro.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (AppShell.Current.FlyoutIsPresented)
            {
                return false;
            }
            else
            {
                AppShell.Current.FlyoutIsPresented = true;
                return true;
            }
        }

        // It handles when the loading is starting
        private void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            ErrorLayout.IsVisible = false;
        }

        // It handles when the loading is finished
        private void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;

            if (e.Result == WebNavigationResult.Failure)
            {
                ErrorLayout.IsVisible = true;
            }
        }

        private void OnReloadClicked(object sender, EventArgs e)
        {
            WebView.Reload();
        }

        private void OnBackButtonClicked(object sender, EventArgs e)
        {
            AppShell.Current.FlyoutIsPresented = true;
        }
    }
}
