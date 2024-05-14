using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups;

public partial class CreateExercisePopup : Popup
{
	public CreateExercisePopup(TrainingPlanViewModel trainingPlanVM)
	{
		InitializeComponent();

		BindingContext = trainingPlanVM;
	}

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}