using System;
namespace GymBro.Models.Entities
{
    public class TrainingPlan
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<WeekDayTrainingPlan> WeekDayTrainingPlan { get; set; } = new List<WeekDayTrainingPlan>();
        //public TrainingPlanExercises TrainingPlanExercises { get; set; } = new TrainingPlanExercises();
        public List<TrainingPlanExercise> Exercises { get; set; } = new List<TrainingPlanExercise>();

        public TrainingPlan(string title)
        {
            Title = title;
        }

        public TrainingPlan()
        {
        }

        public List<TrainingPlanSingleDayModel> ToSingleDayModels()
        {
            return WeekDayTrainingPlan.Select(day => new TrainingPlanSingleDayModel(Id, Title, day, Exercises)).ToList();
        }
    }
}

