using GymBro.ViewModels;

namespace GymBro.Views;

public partial class SettingsPage : ContentPage
{
	private SettingsViewModel _settingsViewModel;
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
		_settingsViewModel = settingsViewModel;
	}
}