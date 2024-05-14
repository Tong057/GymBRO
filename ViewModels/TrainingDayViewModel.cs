using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.Popups;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.Models.Events;
using GymBro.ViewModels.Popups;
using GymBro.Views;
using Microsoft.VisualBasic;

namespace GymBro.ViewModels
{
	public partial class TrainingDayViewModel : ObservableObject
    {
		private TrainingPlan _trainingPlan;
		private TrainingDay _trainingDay;

		private DateTime _startDateTime = DateTime.Now;

		[ObservableProperty]
		private ObservableCollection<KeyValuePair<Exercise, ExerciseStatus>> _exercises = new ObservableCollection<KeyValuePair<Exercise, ExerciseStatus>>();
		

		private Repository _repository;

		public TrainingDayViewModel(Repository repository)
		{
			_repository = repository;

        }

		public async Task InitializeViewModelAsync(int trainingPlanId)
		{
			_trainingPlan = await _repository.GetTrainingPlanById(trainingPlanId);
			TrainingDay savedTrainingDay = await _repository.GetNotEndedTrainingDayForTrainingPlan(_trainingPlan);

			if(savedTrainingDay == null)
			{
                foreach (Exercise exercise in _trainingPlan.TrainingPlanExercises.Exercises)
                {
                    Exercises.Add(new KeyValuePair<Exercise, ExerciseStatus>(exercise, null));
                }
            } else
			{
                foreach (Exercise exercise in _trainingPlan.TrainingPlanExercises.Exercises)
                {
					foreach(ExerciseStatus exerciseStatus in savedTrainingDay.ExerciseStatuses)
					{
						if(exercise == exerciseStatus.Exercise)
						{
                            Exercises.Add(new KeyValuePair<Exercise, ExerciseStatus>(exercise, exerciseStatus));
                        } else
						{
                            Exercises.Add(new KeyValuePair<Exercise, ExerciseStatus>(exercise, null));
                        }
					}
                }

                _trainingDay = savedTrainingDay;
            }
		}

		[RelayCommand]
		public async Task ShowCompleteExercisePopup(Exercise exercise)
		{
			CompletedDataExercisePopupViewModel popupViewModel = new CompletedDataExercisePopupViewModel(exercise);
			popupViewModel.ExerciseDataCollected += OnExerciseDataCollected;
			var popup = new CompletedDataExercisePopup(popupViewModel);

            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

		[RelayCommand]
		public async Task ShowExerciseTrainingInformationPopup(Exercise exercise)
		{
			ExerciseStatus exerciseStatus = await _repository.GetLastExerciseStatus(exercise);
			CompletedDataExerciseInfoPopupViewModel popupViewModel = new CompletedDataExerciseInfoPopupViewModel(exercise, exerciseStatus);
			var popup = new CompletedDataExerciseInfoPopup(popupViewModel);

			await Shell.Current.CurrentPage.ShowPopupAsync(popup);
		}

        [RelayCommand]
        public async Task ClearExerciseStatusData(Exercise exercise)
        {
            var kvExercise = Exercises.FirstOrDefault(kv => kv.Key == exercise);

            if (kvExercise.Key != null)
            {
                Exercises[Exercises.IndexOf(kvExercise)]
                    = new KeyValuePair<Exercise, ExerciseStatus>(exercise, null);

                _trainingDay.ExerciseStatuses.Remove(kvExercise.Value);
                await _repository.UpdateTrainingDay(_trainingDay);

                if (!Exercises.Where(exercise => exercise.Value != null).Any())
                {
                    _repository.DeleteTrainingDay(_trainingDay);
                    _trainingDay = null;
                }
            }
        }

		[RelayCommand]
		public async Task EndTraining()
		{
			if (_trainingDay == null)
				return;

            _trainingDay.EndTime = DateTime.Now;
			await _repository.UpdateTrainingDay(_trainingDay);
			await Shell.Current.Navigation.PopAsync();

        }

        private async void OnExerciseDataCollected(System.Object sender, CompletedDataExerciseArgs args)
		{
			if(_trainingDay == null)
			{
                _trainingDay = new TrainingDay(_trainingPlan);
				_trainingDay.StartTime = _startDateTime;
				await _repository.CreateTrainingDay(_trainingDay);
            }

            var kvExercise = Exercises.FirstOrDefault(kv => kv.Key == args.ExerciseStatus.Exercise);

            if (kvExercise.Key != null)
            {
                Exercises[Exercises.IndexOf(kvExercise)]
                    = new KeyValuePair<Exercise, ExerciseStatus>(args.ExerciseStatus.Exercise, args.ExerciseStatus);

				_trainingDay.ExerciseStatuses.Add(args.ExerciseStatus);
				await _repository.UpdateTrainingDay(_trainingDay);
            }
        }
    }
}

