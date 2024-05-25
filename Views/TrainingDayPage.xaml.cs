using GymBro.ViewModels;

namespace GymBro.Views;

[QueryProperty(nameof(TrainingPlanId), "Id")]
public partial class TrainingDayPage : ContentPage
{
	private readonly TrainingDayViewModel _trainingDayVM;
	public TrainingDayPage(TrainingDayViewModel trainingDayVM)
	{
        InitializeComponent();

        BindingContext = trainingDayVM;
        _trainingDayVM = trainingDayVM;
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
        await _trainingDayVM.InitializeViewModelAsync(trainingPlanId);
    }

}
