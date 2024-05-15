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

        Task<List<Exercise>> GetAllExercises();
        Task<WeekDayTrainingPlan> GetWeekDayTrainingPlanById(int id);
		Task<TrainingPlan> GetTrainingPlanById(int id);
		Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise);
		Task<TrainingDay>? GetNotEndedTrainingDayForTrainingPlan(WeekDayTrainingPlan weekDayTrainingPlan);

		//Delete
		Task DeleteTrainingPlan(TrainingPlan trainingPlan);
		Task DeleteTrainingDay(TrainingDay trainingDay);
    }
}

