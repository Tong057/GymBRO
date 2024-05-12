namespace GymBro.Models.Entities
{
    public class WeekDayTrainingPlan
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }

        public int TrainingPlanId { get; set; }
        public TrainingPlan TrainingPlan { get; set; }


        public WeekDayTrainingPlan(DayOfWeek day)
        {
            Day = day;
        }

        public WeekDayTrainingPlan()
        {
        }
    }
}

