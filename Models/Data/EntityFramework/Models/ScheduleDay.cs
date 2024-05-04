using System;
namespace GymBro.Models.Data.EntityFramework.Models
{
	public class ScheduleDay
	{
		public int Id { get; set; }
		public DayOfWeek Day { get; set; }

		public int TrainingScheduleId { get; set; }
        public TrainingSchedule TrainingSchedule { get; set; }


		public ScheduleDay(DayOfWeek day)
		{
			Day = day;
		}

        public ScheduleDay()
        {
        }
    }
}

