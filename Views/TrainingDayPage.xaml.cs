using GymBro.ViewModels;

namespace GymBro.Views;

[QueryProperty(nameof(TrainingPlanId), "Id")]
public partial class TrainingDayPage : ContentPage
{
	private readonly TrainingDayViewModel _trainingDayViewModel;
	public TrainingDayPage(TrainingDayViewModel trainingDayViewModel)
	{
		InitializeComponent();
        _trainingDayViewModel = trainingDayViewModel;
		BindingContext = trainingDayViewModel;
    }

    public string TrainingPlanId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int trainingPlanId))
            {
                InitializeViewModel(trainingPlanId);
            }
        }
    }

    private async void InitializeViewModel(int trainingPlanId)
    {
        await _trainingDayViewModel.InitializeViewModelAsync(trainingPlanId);
    }

}
