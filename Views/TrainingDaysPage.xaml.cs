using GymBro.ViewModels;

namespace GymBro.Views;

public partial class TrainingDaysPage : ContentPage
{
    private TrainingDaysViewModel _trainingDaysViewModel;
    public TrainingDaysPage(TrainingDaysViewModel trainingDaysViewModel)
    {
        InitializeComponent();

        _trainingDaysViewModel = trainingDaysViewModel;
        BindingContext = trainingDaysViewModel;
    }
}
