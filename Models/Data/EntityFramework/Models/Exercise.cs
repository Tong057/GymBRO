using System;
namespace GymBro.Models.Data.EntityFramework.Models
{
	public class Exercise
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }

        public int TrainingScheduleExerciseId { get; set; }
        public TrainingScheduleExercises TrainingScheduleExercise { get; set; }

        public Exercise(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Exercise()
		{
		}
	}
}

