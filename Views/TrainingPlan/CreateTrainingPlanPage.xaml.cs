using GymBro.ViewModels;

namespace GymBro.Views;

public partial class CreateTrainingPlanPage : ContentPage
{
	public CreateTrainingPlanPage(TrainingPlanViewModel trainingPlanVM)
	{
		InitializeComponent();

        BindingContext = trainingPlanVM;
	}
}