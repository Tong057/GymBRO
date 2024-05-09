using GymBro.Models.Data.EntityFramework.Models;
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
        public async Task CreateTrainingSchedule(TrainingSchedule trainingSchedule)
        {
            await _context.TrainingSchedules.AddAsync(trainingSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTraining(Training training)
        {
            await _context.Trainings.AddAsync(training);
            await _context.SaveChangesAsync();
        }

        


        //Update
        public async Task? UpdateTrainingSchedule(TrainingSchedule trainingSchedule)
        {
            TrainingSchedule findedTrainingSchedule = _context.TrainingSchedules.SingleOrDefault(findTrainingSchedule => findTrainingSchedule.Id == trainingSchedule.Id);
            if (findedTrainingSchedule == null)
                return;

            findedTrainingSchedule.ScheduleDays = trainingSchedule.ScheduleDays;
            findedTrainingSchedule.Title = trainingSchedule.Title;

            await _context.SaveChangesAsync();
        }

        public async Task? UpdateTraining(Training training)
        {
            Training findedTraining = _context.Trainings.SingleOrDefault(findTraining => findTraining.Id == training.Id);
            if (findedTraining == null)
                return;

            findedTraining.StartTime = training.StartTime;
            findedTraining.EndTime = training.EndTime;
            findedTraining.ExerciseStatuses = training.ExerciseStatuses;

            await _context.SaveChangesAsync();
        }


        //Get
        public async Task<List<TrainingSchedule>> GetAllTrainingSchedules()
        {
            return await _context.TrainingSchedules
                .Include(training => training.ScheduleDays)
                .Include(training => training.TrainingScheduleExercises)
                    .ThenInclude(scheduleExercises => scheduleExercises.Exercises)
                .ToListAsync();
        }

        public async Task<ScheduleDay> GetScheduleDayById(int id)
        {
            return await _context.ScheduleDays
                .Where(day => day.Id == id)
                .Include(day => day.TrainingSchedule)
                .SingleAsync();

        }


        //Delete
        public async Task DeleteTrainingSchedule(TrainingSchedule trainingSchedule)
        {
            _context.Remove(trainingSchedule);
            await _context.SaveChangesAsync();
        }
    }
}

