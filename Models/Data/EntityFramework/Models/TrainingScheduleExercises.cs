using System;
namespace GymBro.Models.Data.EntityFramework.Models
{
	public class TrainingScheduleExercises
	{
		public int Id { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

		public int TrainingScheduleId { get; set; }
		public TrainingSchedule TrainingSchedule { get; set; }

        public TrainingScheduleExercises(ICollection<Exercise> exercises)
        {
			Exercises = exercises;
        }

        public TrainingScheduleExercises()
		{
		}
	}
}

