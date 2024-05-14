using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups;

public partial class SaveTrainingPlanPopup : Popup
{
	public SaveTrainingPlanPopup(TrainingPlanViewModel trainingPlanVM)
	{
		InitializeComponent();

		BindingContext = trainingPlanVM;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await this.CloseAsync();
    }
}