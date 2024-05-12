using System;
namespace GymBro.Models.Entities
{
    public class ExerciseStatus
    {
        public int Id { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }

        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public Note Note { get; set; }

        public ICollection<ExerciseWeight> ExerciseWeights { get; set; } = new List<ExerciseWeight>();

        public ExerciseStatus(Exercise exercise, Note note)
        {
            ExerciseId = exercise.Id;
            Exercise = exercise;
            Note = note;
        }

        public ExerciseStatus(Exercise exercise)
        {
            ExerciseId = exercise.Id;
            Exercise = exercise;
        }

        public ExerciseStatus()
        {
        }
    }
}

