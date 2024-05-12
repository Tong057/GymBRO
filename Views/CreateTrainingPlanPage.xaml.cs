using GymBro.ViewModels;

namespace GymBro.Views;

public partial class CreateTrainingPlanPage : ContentPage
{
	CreateTrainingPlanViewModel _createTrainingPlanVM;
	public CreateTrainingPlanPage(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();
        BindingContext = createTrainingPlanVM;
		_createTrainingPlanVM = createTrainingPlanVM;
	}
}