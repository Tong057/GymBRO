using GymBro.ViewModels;

namespace GymBro.Views;

[QueryProperty(nameof(WeekDayTrainingPlanId), "Id")]
public partial class EditTrainingPlanPage : ContentPage
{
    private TrainingPlanViewModel _trainingPlanVM;

    public EditTrainingPlanPage(TrainingPlanViewModel trainingPlanVM)
	{
		InitializeComponent();

        BindingContext = trainingPlanVM;
        _trainingPlanVM = trainingPlanVM;
	}

    public string WeekDayTrainingPlanId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int planId))
            {
                LoadPlan(planId);
            }
        }
    }

    private async void LoadPlan(int planId)
    {
        await _trainingPlanVM.LoadPlanAsync(planId);
    }
}