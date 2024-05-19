using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.Views;

namespace GymBro.ViewModels
{
    public partial class TrainingDaysViewModel : ObservableObject
    {
        private Repository _repository;

        public TrainingDaysViewModel(Repository repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        private ObservableCollection<TrainingPlan> _trainingPlans;

        [ObservableProperty]
        private bool _isTrainingPlansEmpty = false;

        [RelayCommand]
        private async Task GoToCreateTrainingPlan()
        {
            await Shell.Current.GoToAsync(nameof(CreateTrainingPlanPage));
        }

        [RelayCommand]
        private async Task GoToTrainingDayPage(int trainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(TrainingDayPage)}?Id={trainingPlanId}");
        }

        [RelayCommand]
        private async Task GoToEditTrainingPlan(int trainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(EditTrainingPlanPage)}?Id={trainingPlanId}");
        }

        [RelayCommand]
        private async Task DeleteTrainingPlan(TrainingPlan trainingPlan)
        {
            TrainingPlans.Remove(trainingPlan);
            await _repository.DeleteTrainingPlan(trainingPlan);
        }

        private void InitializeCollection()
        {
            TrainingPlans = new ObservableCollection<TrainingPlan>();
            IsTrainingPlansEmpty = !TrainingPlans.Any();
            TrainingPlans.CollectionChanged += (s, e) => IsTrainingPlansEmpty = !TrainingPlans.Any();
        }

        public async Task LoadTrainingPlans()
        {
            InitializeCollection();

            DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            IEnumerable<TrainingPlan> trainingPlans = await _repository.GetAllTrainingPlans();
            foreach (TrainingPlan plan in trainingPlans.OrderBy(plan => ((plan.Day - firstDayOfWeek + 7) % 7)))
            {
                TrainingPlans.Add(plan);
            }
        }
    }
}

