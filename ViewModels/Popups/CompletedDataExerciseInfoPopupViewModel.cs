using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Entities;

namespace GymBro.ViewModels.Popups
{
	public partial class CompletedDataExerciseInfoPopupViewModel : ObservableObject
	{

		[ObservableProperty]
		private Exercise _exercise;

		[ObservableProperty]
		private ExerciseStatus _lastExerciseStatus;

		public CompletedDataExerciseInfoPopupViewModel(Exercise exercise, ExerciseStatus lastExerciseStatus)
		{
			Exercise = exercise;
			LastExerciseStatus = lastExerciseStatus;
		}
	}
}

