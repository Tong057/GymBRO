using System;
using GymBro.Models.Data.EntityFramework.Models;

namespace GymBro.Models
{
	public class TrainingScheduleSingleDayModel
	{
        public int Id { get; set; }
        public string Title { get; set; }

        public ScheduleDay ScheduleDay { get; set; }
        public TrainingScheduleExercises TrainingScheduleExercises { get; set; }

        public TrainingScheduleSingleDayModel(int id, string title, ScheduleDay scheduleDay, TrainingScheduleExercises trainingScheduleExercises)
        {
            Id = id;
            Title = title;
            ScheduleDay = scheduleDay;
            TrainingScheduleExercises = trainingScheduleExercises;
        }
	}
}

