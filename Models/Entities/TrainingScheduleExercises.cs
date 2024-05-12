using System;
namespace GymBro.Models.Entities
{
    public class TrainingScheduleExercises
    {
        public int Id { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

        public int TrainingScheduleId { get; set; }
        public TrainingSchedule TrainingSchedule { get; set; }

        public TrainingScheduleExercises()
        {
        }
    }
}

