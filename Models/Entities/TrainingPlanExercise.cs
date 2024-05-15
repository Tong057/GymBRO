using CommunityToolkit.Mvvm.ComponentModel;

namespace GymBro.Models.Entities
{
    public partial class TrainingPlanExercise
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public int ExerciseId { get; set; }
        public int Position { get; set; }

        public TrainingPlanExercise() { }

        public TrainingPlanExercise(Exercise exercise, int position) 
        {
            Exercise = exercise;
            Position = position;
        }

    }
}
