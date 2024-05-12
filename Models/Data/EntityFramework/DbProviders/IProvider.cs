using GymBro.Models.Entities;

namespace GymBro.Models.Data.EntityFramework.DbProviders
{
    public interface IProvider
	{

		//Create
		Task CreateTrainingPlan(TrainingPlan trainingPlan);
		Task CreateTrainingDay(TrainingDay trainingDay);


		//Update
		Task UpdateTrainingPlan(TrainingPlan trainingPlan);
		Task UpdateTrainingDay(TrainingDay trainingDay);

		//Get
		Task<List<TrainingPlan>> GetAllTrainingPlans();
		Task<WeekDayTrainingPlan> GetWeekDayTrainingPlanById(int id);
		Task<TrainingPlan> GetTrainingPlanById(int id);

		//Delete
		Task DeleteTrainingPlan(TrainingPlan trainingPlan);

    }
}

