using CommunityToolkit.Mvvm.ComponentModel;
namespace GymBro.Models.Entities
{
    public partial class TrainingPlan : ObservableObject
    {
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private DayOfWeek _day;
        public DayOfWeek Day
        {
            get => _day;
            set => SetProperty(ref _day, value);
        }

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

