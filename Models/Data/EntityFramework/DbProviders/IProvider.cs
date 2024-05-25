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
		Task<TrainingPlan> GetTrainingPlanById(int id);
		Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise);
		Task<TrainingDay>? GetNotEndedTrainingDayForTrainingPlan(TrainingPlan trainingPlan);
		Task<List<ExerciseStatus>> GetAllExerciseStatuses();
		Task<List<ExerciseStatus>> GetExerciseStatusesByExercise(Exercise exercise);

        //Delete
        Task DeleteTrainingPlan(TrainingPlan trainingPlan);
		Task DeleteTrainingDay(TrainingDay trainingDay);
		Task DeleteExerciseStatus(ExerciseStatus exerciseStatus);
		void ClearAllData();
    }
}

