using System;

namespace GymBro.Models.Entities
{
    public class TrainingPlanSingleDayModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public WeekDayTrainingPlan WeekDayTrainingPlan { get; set; }
        public TrainingPlanExercises TrainingPlanExercises { get; set; }

        public TrainingPlanSingleDayModel(int id, string title, WeekDayTrainingPlan weekDayTrainingPlan, TrainingPlanExercises trainingPlanExercises)
        {
            Id = id;
            Title = title;
            WeekDayTrainingPlan = weekDayTrainingPlan;
            TrainingPlanExercises = trainingPlanExercises;
        }
    }
}

