using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Entities;
using GymBro.Models.Events;
using GymBro.ViewModels.Popups;

namespace GymBro.Controls.Popups;

public partial class CompletedDataExercisePopup : Popup
{
    CompletedDataExercisePopupViewModel _completedDataExercisePopupViewModel;

    public CompletedDataExercisePopup(CompletedDataExercisePopupViewModel completedDataExercisePopupViewModel)
	{
		InitializeComponent();
		BindingContext = completedDataExercisePopupViewModel;
		completedDataExercisePopupViewModel.Popup = this;
        _completedDataExercisePopupViewModel = completedDataExercisePopupViewModel;
    }
}