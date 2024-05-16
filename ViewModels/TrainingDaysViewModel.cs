using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.Views;

namespace GymBro.ViewModels
{
    public partial class TrainingDaysViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TrainingPlan> _trainingPlans = new ObservableCollection<TrainingPlan>();

        [ObservableProperty]
        private bool _isTrainingPlansEmpty = false;

        private Repository _repository;

        public TrainingDaysViewModel(Repository repository)
        {
            _repository = repository;

            InitializeViewModel();
        }

        [RelayCommand]
        public async Task GoToCreateTrainingPlan()
        {
            await Shell.Current.GoToAsync(nameof(CreateTrainingPlanPage));
        }

        [RelayCommand]
        public async Task GoToTrainingDayPage(int trainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(TrainingDayPage)}?Id={trainingPlanId}");
        }

        [RelayCommand]
        public async Task GoToEditTrainingPlan(int trainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(EditTrainingPlanPage)}?Id={trainingPlanId}");
        }

        [RelayCommand]
        public async Task DeleteTrainingPlan(TrainingPlan trainingPlan)
        {
            TrainingPlans.Remove(trainingPlan);
            await _repository.DeleteTrainingPlan(trainingPlan);
        }

        public async void InitializeViewModel()
        {
            LoadTrainingPlans(await _repository.GetAllTrainingPlans());

            IsTrainingPlansEmpty = !TrainingPlans.Any();
            TrainingPlans.CollectionChanged += (s, e) => IsTrainingPlansEmpty = !TrainingPlans.Any();
        }

        public void LoadTrainingPlans(IEnumerable<TrainingPlan> trainingPlans)
        {
            TrainingPlans.Clear();

            foreach (TrainingPlan plan in trainingPlans.OrderBy(plan => plan.Day))
            {
                TrainingPlans.Add(plan);
            }

        }

        public async Task LoadTrainingPlans()
        {
            LoadTrainingPlans(await _repository.GetAllTrainingPlans());
        }
    }
}

