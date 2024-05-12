using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.Views;

namespace GymBro.ViewModels
{
    public partial class CreateTrainingPlanViewModel : ObservableObject
    {
        public CreateTrainingPlanViewModel() 
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

        [RelayCommand]
        public async Task OpedAddExerciseDialog()
        {
            
        }
    }
}
