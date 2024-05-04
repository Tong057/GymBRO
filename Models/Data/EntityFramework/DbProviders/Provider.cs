using GymBro.Models.Data.EntityFramework.Models;

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
            _context.TrainingSchedules.Add(trainingSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTraining(Training training)
        {
            _context.Trainings.Add(training);
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
    }
}

