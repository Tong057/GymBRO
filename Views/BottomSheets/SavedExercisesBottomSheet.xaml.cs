using GymBro.ViewModels;
using The49.Maui.BottomSheet;

namespace GymBro.Views.BottomSheets;

public partial class SavedExercisesBottomSheet : BottomSheet
{
	public SavedExercisesBottomSheet(CreateTrainingPlanViewModel createTrainingPlanVM)
	{
		InitializeComponent();

		BindingContext = createTrainingPlanVM;
	}
}