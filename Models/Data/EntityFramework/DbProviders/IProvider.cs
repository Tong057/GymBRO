using System;
using GymBro.Models.Data.EntityFramework.Models;

namespace GymBro.Models.Data.EntityFramework.DbProviders
{
	public interface IProvider
	{

		//Create
		Task CreateTrainingSchedule(TrainingSchedule trainingSchedule);


		//Update
		Task UpdateTrainingSchedule(TrainingSchedule trainingSchedule);
	}
}

