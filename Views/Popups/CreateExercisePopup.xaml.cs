using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups;

public partial class CreateExercisePopup : Popup
{
	public CreateExercisePopup(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
	}

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}