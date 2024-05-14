using CommunityToolkit.Maui.Views;
using GymBro.ViewModels;
using GymBro.ViewModels.Popups;

namespace GymBro.Controls.Popups;

public partial class CompletedDataExerciseInfoPopup : Popup
{
    public CompletedDataExerciseInfoPopup(CompletedDataExerciseInfoPopupViewModel completedDataExerciseInfoPopupViewModel)
	{
		InitializeComponent();
		BindingContext = completedDataExerciseInfoPopupViewModel;
    }
}