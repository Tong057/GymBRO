namespace GymBro.Models.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TrainingPlanExerciseId { get; set; }
        public TrainingPlanExercises TrainingPlanExercise { get; set; }

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

