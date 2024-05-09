using System;
namespace GymBro.Models.Data.EntityFramework.Models
{
	public class TrainingSchedule
	{
		
		public int Id { get; set; }
		public string Title { get; set; }

        public ICollection<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();
		public TrainingScheduleExercises TrainingScheduleExercises { get; set; } = new TrainingScheduleExercises();

        public TrainingSchedule(string title)
		{
			Title = title;
		}

        public TrainingSchedule()
        {
        }

		public List<TrainingScheduleSingleDayModel> ToSingleDayModels()
		{
			return ScheduleDays.Select(day => new TrainingScheduleSingleDayModel(Id, Title, day, TrainingScheduleExercises)).ToList();
		}
    }
}

