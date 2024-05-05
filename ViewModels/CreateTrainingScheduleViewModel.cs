using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Data.EntityFramework.Models;
using System.Collections.ObjectModel;

namespace GymBro.ViewModels
{
    public partial class CreateTrainingScheduleViewModel : ObservableObject
    {
        public CreateTrainingScheduleViewModel() 
        {
            _exercises = [
                new Exercise { Name = "Pull up", Description = "Blablabla" },
                new Exercise { Name = "Squads", Description = "Blablabla" },
                new Exercise { Name = "Bench Press", Description = "Blablabla" },
                new Exercise { Name = "Shraugs", Description = "Blablabla" },
            ];
        }

        [ObservableProperty]
        public ObservableCollection<Exercise> _exercises;
    }
}
