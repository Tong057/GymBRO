﻿using GymBro.Models.Data.EntityFramework.DbProviders;
using GymBro.Models.Entities;
using SQLitePCL;

namespace GymBro.Models.Data.EntityFramework.Repositories
{
    public class Repository
	{
        private IProvider _provider;

        public Repository(IProvider provider)
		{
			_provider = provider;
		}

        //Create
        public async Task CreateTrainingPlan(TrainingPlan trainingPlan)
        {
            await _provider.CreateTrainingPlan(trainingPlan);
        }

        public async Task CreateTrainingDay(TrainingDay trainingDay)
        {
            await _provider.CreateTrainingDay(trainingDay);
        }

        //Update
        public async Task? UpdateTrainingPlan(TrainingPlan trainingPlan)
        {
            await _provider.UpdateTrainingPlan(trainingPlan);
        }

        public async Task? UpdateTrainingDay(TrainingDay trainingDay)
        {
            await _provider.UpdateTrainingDay(trainingDay);
        }

        //Get
        public async Task<List<TrainingPlan>> GetAllTrainingPlans()
        {
            return await _provider.GetAllTrainingPlans();
        }

        public async Task<List<Exercise>> GetAllExercises()
        {
            return await _provider.GetAllExercises();
        }

        public async Task<TrainingPlan> GetTrainingPlanById(int id)
        {
            return await _provider.GetTrainingPlanById(id);
        }

        public async Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise)
        {
            return await _provider.GetLastExerciseStatus(exercise);
        }

        public async Task<List<ExerciseStatus>> GetExerciseStatusesByExercise(Exercise exercise)
        {
            return await _provider.GetExerciseStatusesByExercise(exercise);
        }

        public async Task<TrainingDay>? GetNotEndedTrainingDayForTrainingPlan(TrainingPlan trainingPlan)
        {
            return await _provider.GetNotEndedTrainingDayForTrainingPlan(trainingPlan);
        }

        public async Task<List<ExerciseStatus>> GetAllExerciseStatuses()
        {
            return await _provider.GetAllExerciseStatuses();
        }

        //Delete
        public async Task DeleteTrainingPlan(TrainingPlan trainingPlan)
        {
            await _provider.DeleteTrainingPlan(trainingPlan);
        }

        public async Task DeleteTrainingDay(TrainingDay trainingDay)
        {
            await _provider.DeleteTrainingDay(trainingDay);
        }

        public async Task DeleteExerciseStatus(ExerciseStatus exerciseStatus)
        {
            await _provider.DeleteExerciseStatus(exerciseStatus);
        }

        public void ClearAllData()
        {
            _provider.ClearAllData();
        }
    }
}

