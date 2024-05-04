﻿using System;
using GymBro.Models.Data.EntityFramework.DbProviders;
using GymBro.Models.Data.EntityFramework.Models;

namespace GymBro.Models.Data.EntityFramework.Repositories
{
	public class Repository
	{
        private IProvider _provider;

        public Repository(IProvider provider)
		{
			_provider = provider;
		}

        //Create
        public async Task CreateTrainingSchedule(TrainingSchedule trainingSchedule)
        {
            await _provider.CreateTrainingSchedule(trainingSchedule);
        }

        public async Task CreateTraining(Training training)
        {
            await _provider.CreateTraining(training);
        }

        //Update
        public async Task? UpdateTrainingSchedule(TrainingSchedule trainingSchedule)
        {
            await _provider.UpdateTrainingSchedule(trainingSchedule);
        }

        public async Task? UpdateTraining(Training training)
        {
            await _provider.UpdateTraining(training);
        }
    }
}

