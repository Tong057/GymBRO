using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups.Exercise;

public partial class EditExercisePopup : Popup
{
	public EditExercisePopup(TrainingPlanViewModel trainingPlanVM)
	{
		InitializeComponent();

		BindingContext = trainingPlanVM;
	}

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}