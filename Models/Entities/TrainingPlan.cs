using System;
namespace GymBro.Models.Entities
{
    public class TrainingPlan
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DayOfWeek Day { get; set; }

        public List<TrainingPlanExercise> Exercises { get; set; } = new List<TrainingPlanExercise>();

        public TrainingPlan(string title, DayOfWeek day)
        {
            Title = title;
            Day = day;
        }

        public TrainingPlan()
        {
        }
    }
}

