using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups;

public partial class SaveTrainingPlanPopup : Popup
{
	public SaveTrainingPlanPopup(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await this.CloseAsync();
    }
}