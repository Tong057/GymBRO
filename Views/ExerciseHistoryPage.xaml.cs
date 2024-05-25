using GymBro.ViewModels;

namespace GymBro.Views;

public partial class ExerciseHistoryPage : ContentPage
{
    private ExerciseHistoryViewModel _exerciseHistoryVM;

	public ExerciseHistoryPage(ExerciseHistoryViewModel exerciseHistoryVM)
	{
		InitializeComponent();

		BindingContext = exerciseHistoryVM;
        _exerciseHistoryVM = exerciseHistoryVM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _exerciseHistoryVM.LoadSavedExercises();
    }
}