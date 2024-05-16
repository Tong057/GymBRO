using CommunityToolkit.Mvvm.ComponentModel;
namespace GymBro.Models.Entities
{
    public partial class TrainingPlan : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private DayOfWeek _day;

        public ICollection<TrainingDay> TrainingDays { get; set; } = new List<TrainingDay>();
        public ICollection<TrainingPlanExercise> Exercises { get; set; } = new List<TrainingPlanExercise>();

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

