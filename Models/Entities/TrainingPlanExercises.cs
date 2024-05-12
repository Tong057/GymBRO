namespace GymBro.Models.Entities
{
    public class TrainingPlanExercises
    {
        public int Id { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

        public int TrainingPlanId { get; set; }
        public TrainingPlan TrainingPlan { get; set; }

        public TrainingPlanExercises()
        {
        }
    }
}

