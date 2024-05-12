using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;

namespace GymBro.ViewModels
{
	public class TrainingDayViewModel : ObservableObject
    {
		private TrainingPlan _trainingPlan;
		private Repository _repository;

		public TrainingDayViewModel(Repository repository)
		{
			_repository = repository;
		}

		public async Task InitializeViewModelAsync(int trainingPlanId)
		{
			_trainingPlan = await _repository.GetTrainingPlanById(trainingPlanId);
		}

    }
}

