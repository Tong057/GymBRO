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
        public string? Description { get; set; }

        public Exercise(string name)
        {
            Name = name;
        }

        public Exercise(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Exercise()
        {
        }


        public override string ToString()
        {
            return Name;
        }
        [ObservableProperty]
        private bool _isBeingDragged;

        [ObservableProperty]
        private bool _isBeingDraggedOver;

        public Exercise Clone()
        {
            return new Exercise(Name, Description);
        }
    }
}

