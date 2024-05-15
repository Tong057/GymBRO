namespace GymBro.Models.Entities
{
    public class TrainingDay
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DayOfWeek Day { get; set; }

        public ICollection<ExerciseStatus> ExerciseStatuses { get; set; } = new List<ExerciseStatus>();

        public int TrainingPlanId { get; set; }
        public TrainingPlan TrainingPlan { get; set; }

        public TrainingDay(TrainingPlan trainingPlan, DayOfWeek day)
        {
            TrainingPlanId = trainingPlan.Id;
            TrainingPlan = trainingPlan;
            Day = day;
        }

        public TrainingDay()
        {
        }
    }
}

