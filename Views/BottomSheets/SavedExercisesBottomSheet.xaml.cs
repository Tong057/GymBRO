using GymBro.ViewModels;
using The49.Maui.BottomSheet;

namespace GymBro.Views.BottomSheets;

public partial class SavedExercisesBottomSheet : BottomSheet
{
	public SavedExercisesBottomSheet(TrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
	}

    public SavedExercisesBottomSheet(StatisticsViewModel statisticsVM)
    {
        InitializeComponent();

        BindingContext = statisticsVM;
    }
}