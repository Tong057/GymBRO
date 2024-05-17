using GymBro.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymBro.Models.Data.EntityFramework.DbProviders
{
    public class Provider : IProvider
	{
		private ApplicationContext _context;

		public Provider(ApplicationContext applicationContext)
		{
			_context = applicationContext;
		}

        //Create
        public async Task CreateTrainingPlan(TrainingPlan trainingPlan)
        {
            await _context.TrainingPlans.AddAsync(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTrainingDay(TrainingDay trainingDay)
        {
            await _context.TrainingDays.AddAsync(trainingDay);
            await _context.SaveChangesAsync();
        }

        //Update
        public async Task? UpdateTrainingPlan(TrainingPlan trainingPlan)
        {
            TrainingPlan findedTrainingPlan = _context.TrainingPlans.SingleOrDefault(findTrainingPlan => findTrainingPlan.Id == trainingPlan.Id);
            if (findedTrainingPlan == null)
                return;

            findedTrainingPlan.Title = trainingPlan.Title;

            await _context.SaveChangesAsync();
        }

        public async Task? UpdateTrainingDay(TrainingDay trainingDay)
        {
            TrainingDay findedTraining = _context.TrainingDays.SingleOrDefault(findTrainingDay => findTrainingDay.Id == trainingDay.Id);
            if (findedTraining == null)
                return;

            findedTraining.StartTime = trainingDay.StartTime;
            findedTraining.EndTime = trainingDay.EndTime;
            findedTraining.ExerciseStatuses = trainingDay.ExerciseStatuses;

            await _context.SaveChangesAsync();
        }

        //Get
        public async Task<List<TrainingPlan>> GetAllTrainingPlans()
        {
            return await _context.TrainingPlans
                .Include(training => training.Exercises)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetAllExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<TrainingPlan> GetTrainingPlanById(int id)
        {
            return await _context.TrainingPlans
                .Where(plan => plan.Id == id)
                .Include(plan => plan.Exercises)
                    .ThenInclude(exercise => exercise.Exercise)
                .SingleAsync();
        }

        public async Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise)
        {
            return await _context.ExerciseStatuses
                .Include(exerciseStatus => exerciseStatus.Note)
                .Include(exerciseStatus => exerciseStatus.ExerciseSets)
                .Where(exerciseStatus => exerciseStatus.ExerciseId == exercise.Id)
                .Where(exerciseStatus => exerciseStatus.TrainingDay.EndTime != null)
                .OrderByDescending(exerciseStatus => exerciseStatus.TrainingDay.EndTime)
                .FirstOrDefaultAsync();
        }

        public async Task<TrainingDay>? GetNotEndedTrainingDayForTrainingPlan(TrainingPlan trainingPlan)
        {
            return await _context.TrainingDays
                .Include(trainingDay => trainingDay.ExerciseStatuses)
                .Where(trainingDay => trainingDay.TrainingPlanId == trainingPlan.Id)
                .Where(trainingDay => trainingDay.EndTime == null)
                .OrderByDescending(trainingDay => trainingDay.EndTime)
                .SingleOrDefaultAsync();
        }

        public async Task<List<ExerciseStatus>> GetAllExerciseStatuses()
        {
            return await _context.ExerciseStatuses.ToListAsync();
        }

        public async Task<List<ExerciseStatus>> GetExerciseStatusesByExercise(Exercise exercise)
        {
            return await _context.ExerciseStatuses
                .Include(exStat => exStat.TrainingDay)
                .Include(exStat => exStat.ExerciseSets)
                .Where(exStat => exStat.ExerciseId == exercise.Id)
                .ToListAsync();
        }

        //Delete
        public async Task DeleteTrainingPlan(TrainingPlan trainingPlan)
        {
            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTrainingDay(TrainingDay trainingDay)
        {
            _context.TrainingDays.Remove(trainingDay);
            await _context.SaveChangesAsync();
        }
    }
}

