using GymBro.Models.Data.EntityFramework.Models;

namespace GymBro.Models.Data.EntityFramework.DbProviders
{
	public interface IProvider
	{

		//Create
		Task CreateTrainingSchedule(TrainingSchedule trainingSchedule);
		Task CreateTraining(Training training);


		//Update
		Task UpdateTrainingSchedule(TrainingSchedule trainingSchedule);
		Task UpdateTraining(Training training);

		//Get
		Task<List<TrainingSchedule>> GetAllTrainingSchedules();
		Task<ScheduleDay> GetScheduleDayById(int id);

		//Delete
		Task DeleteTrainingSchedule(TrainingSchedule trainingSchedule);

    }
}

