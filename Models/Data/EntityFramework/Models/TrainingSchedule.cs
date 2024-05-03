using System;
namespace GymBro.Models.Data.EntityFramework.Models
{
	public class TrainingSchedule
	{
		
		public int Id { get; set; }
		public string Title { get; set; }

		public ICollection<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();


		public TrainingSchedule(string title)
		{
			Title = title;
		}

        public TrainingSchedule()
        {
        }
    }
}

