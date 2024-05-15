using GymBro.Models.Data.EntityFramework.DbProviders;
using GymBro.Models.Entities;

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

        public async Task<WeekDayTrainingPlan> GetWeekDayTrainingPlanById(int id)
        {
            return await _provider.GetWeekDayTrainingPlanById(id);
        }

        public async Task<TrainingPlan> GetTrainingPlanById(int id)
        {
            return await _provider.GetTrainingPlanById(id);
        }

        public async Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise)
        {
            return await _provider.GetLastExerciseStatus(exercise);
        }

        public async Task<TrainingDay>? GetNotEndedTrainingDayForTrainingPlan(WeekDayTrainingPlan weekDayTrainingPlan)
        {
            return await _provider.GetNotEndedTrainingDayForTrainingPlan(weekDayTrainingPlan);
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
    }
}

