using GymBro.ViewModels;

namespace GymBro.Views;

[QueryProperty(nameof(TrainingScheduleId), "Id")]
public partial class TrainingDayPage : ContentPage
{
	private readonly TrainingDayViewModel _trainingDayViewModel;
	public TrainingDayPage(TrainingDayViewModel trainingDayViewModel)
	{
		InitializeComponent();
        _trainingDayViewModel = trainingDayViewModel;
		BindingContext = trainingDayViewModel;
    }

    public string TrainingScheduleId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int trainingScheduleId))
            {
                InitializeViewModel(trainingScheduleId);
            }
        }
    }

    private async void InitializeViewModel(int contactId)
    {
        await _trainingDayViewModel.InitializeViewModelAsync(contactId);
    }

}
