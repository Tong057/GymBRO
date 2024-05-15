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

            findedTrainingPlan.WeekDayTrainingPlan = trainingPlan.WeekDayTrainingPlan;
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
                .Include(training => training.WeekDayTrainingPlan)
                .Include(training => training.Exercises)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetAllExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<WeekDayTrainingPlan> GetWeekDayTrainingPlanById(int id)
        {
            return await _context.WeekDayTrainingPlans
                .Where(dayPlan => dayPlan.Id == id)
                .Include(dayPlan => dayPlan.TrainingPlan)
                    .ThenInclude(trainingPlan => trainingPlan.WeekDayTrainingPlan)
                .Include(dayPlan => dayPlan.TrainingPlan)
                    .ThenInclude(trainingPlan => trainingPlan.Exercises)
                .SingleAsync();

        }

        public async Task<TrainingPlan> GetTrainingPlanById(int id)
        {
            return await _context.TrainingPlans
                .Where(plan => plan.Id == id)
                .Include(plan => plan.WeekDayTrainingPlan)
                .Include(plan => plan.Exercises)
                .SingleAsync();
        }

        public async Task<ExerciseStatus>? GetLastExerciseStatus(Exercise exercise)
        {
            return await _context.ExerciseStatuses
                .Include(exerciseStatus => exerciseStatus.Note)
                .Include(exerciseStatus => exerciseStatus.ExerciseWeights)
                .OrderByDescending(exerciseStatus => exerciseStatus.TrainingDay.EndTime)
                .FirstOrDefaultAsync(exerciseStatus => exerciseStatus.ExerciseId == exercise.Id);
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

