using CommunityToolkit.Maui.Views;
using GymBro.Models.Entities;
using GymBro.ViewModels;

namespace GymBro.Controls.Popups;

public partial class CompletedDataExercisePopup : Popup
{
	private readonly TrainingDayViewModel _trainingDayViewModel;
	private readonly Exercise _exercise;
	public CompletedDataExercisePopup(TrainingDayViewModel trainingDayViewModel, Exercise exercise)
	{
		InitializeComponent();

		BindingContext = trainingDayViewModel;
        _trainingDayViewModel = trainingDayViewModel;
		_exercise = exercise;
	}
}