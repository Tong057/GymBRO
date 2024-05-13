using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Views.Popups;

public partial class EditExercisePopup : Popup
{
	public EditExercisePopup(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
	}

    private async void Close_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}