using CommunityToolkit.Mvvm.ComponentModel;

namespace GymBro.Models.Entities
{
    public partial class Exercise : ObservableObject
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Description { get; set; }

        public int TrainingPlanExerciseId { get; set; }

        [ObservableProperty]
        private bool _isBeingDragged;
        [ObservableProperty]
        private bool _isBeingDraggedOver;

        public TrainingPlanExercises TrainingPlanExercise { get; set; }

        public Exercise(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Exercise()
        {
        }

        public Exercise Clone()
        {
            return new Exercise(Name, Description);
        }
    }
}

