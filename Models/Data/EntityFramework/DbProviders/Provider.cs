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
                .Include(training => training.TrainingPlanExercises)
                    .ThenInclude(planExercises => planExercises.Exercises)
                .ToListAsync();
        }

        public async Task<WeekDayTrainingPlan> GetWeekDayTrainingPlanById(int id)
        {
            return await _context.WeekDayTrainingPlans
                .Where(day => day.Id == id)
                .Include(day => day.TrainingPlan)
                .SingleAsync();
        }

        public async Task<TrainingPlan> GetTrainingPlanById(int id)
        {
            return await _context.TrainingPlans
                .Where(plan => plan.Id == id)
                .Include(plan => plan.TrainingPlanExercises)
                .Include(plan => plan.WeekDayTrainingPlan)
                .SingleAsync();
        }

        //Delete
        public async Task DeleteTrainingPlan(TrainingPlan trainingPlan)
        {
            _context.Remove(trainingPlan);
            await _context.SaveChangesAsync();
        }
    }
}

