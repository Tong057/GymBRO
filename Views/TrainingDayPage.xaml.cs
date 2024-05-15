using GymBro.ViewModels;

namespace GymBro.Views;

[QueryProperty(nameof(WeekDayTrainingPlanId), "Id")]
public partial class TrainingDayPage : ContentPage
{
	private readonly TrainingDayViewModel _trainingDayViewModel;
	public TrainingDayPage(TrainingDayViewModel trainingDayViewModel)
	{
		InitializeComponent();
        _trainingDayViewModel = trainingDayViewModel;
		BindingContext = trainingDayViewModel;
    }

    public string WeekDayTrainingPlanId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int weekDayTrainingPlanId))
            {
                InitializeViewModel(weekDayTrainingPlanId);
            }
        }
    }

    private async void InitializeViewModel(int weekDayTrainingPlanId)
    {
        await _trainingDayViewModel.InitializeViewModelAsync(weekDayTrainingPlanId);
    }

}
