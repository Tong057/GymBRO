using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;

namespace GymBro.ViewModels
{
	public partial class TrainingDayViewModel : ObservableObject
    {
		private TrainingPlan _trainingPlan;
		private TrainingDay _trainingDay;

		[ObservableProperty]
		private Dictionary<Exercise, ExerciseStatus> _exercises = new Dictionary<Exercise, ExerciseStatus>();
		

		private Repository _repository;

		public TrainingDayViewModel(Repository repository)
		{
			_repository = repository;
		}

		public async Task InitializeViewModelAsync(int trainingPlanId)
		{
			_trainingPlan = await _repository.GetTrainingPlanById(trainingPlanId);
			_trainingDay = new TrainingDay(_trainingPlan);

			foreach(Exercise exercise in _trainingPlan.TrainingPlanExercises.Exercises)
			{
				Exercises.Add(exercise, new ExerciseStatus(exercise));
			}
		}

    }
}

