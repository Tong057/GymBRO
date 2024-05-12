using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;

namespace GymBro.Controls.Popups;

public partial class CreateExercisePopup : Popup
{
	CreateTrainingPlanViewModel _createTrainingPlanVM;
	public CreateExercisePopup(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
		_createTrainingPlanVM = createTrainingPlanVM;
	}
}