using System;
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
    }
}

