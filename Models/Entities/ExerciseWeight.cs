using System;
namespace GymBro.Models.Entities
{
    public class ExerciseWeight
    {
        public int Id { get; set; }
        public double? Weight { get; set; }
        public int? Repeats { get; set; }

        public int? ExerciseStatusId { get; set; }
        public ExerciseStatus? ExerciseStatus { get; set; }

        public ExerciseWeight(double? weight, int? repeats)
        {
            Weight = weight;
            Repeats = repeats;
        }

        public ExerciseWeight()
        {
        }
    }
}

