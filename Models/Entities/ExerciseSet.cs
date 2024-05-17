using System;
namespace GymBro.Models.Entities
{
    public class ExerciseSet
    {
        public int Id { get; set; }
        public double? Weight { get; set; }
        public int? Repeats { get; set; }

        public int? ExerciseStatusId { get; set; }
        public ExerciseStatus? ExerciseStatus { get; set; }

        public ExerciseSet(double? weight, int? repeats)
        {
            Weight = weight;
            Repeats = repeats;
        }

        public ExerciseSet()
        {
        }
    }
}

