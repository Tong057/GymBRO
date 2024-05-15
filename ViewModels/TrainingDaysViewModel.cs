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
        private ObservableCollection<TrainingPlanSingleDayModel> _trainingPlans = new ObservableCollection<TrainingPlanSingleDayModel>();

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
        public async Task GoToTrainingDayPage(int weekDayTrainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(TrainingDayPage)}?Id={weekDayTrainingPlanId}");
        }

        [RelayCommand]
        public async Task GoToEditWeekDayTrainingPlan(int weekDayTrainingPlanId)
        {
            await Shell.Current.GoToAsync($"{nameof(EditTrainingPlanPage)}?Id={weekDayTrainingPlanId}");
        }

        [RelayCommand]
        public async Task DeleteWeekDayTrainingPlan(int weekDayTrainingPlanId)
        {
            WeekDayTrainingPlan weekDayTrainingPlan = await _repository.GetWeekDayTrainingPlanById(weekDayTrainingPlanId);
            if (weekDayTrainingPlan == null)
                return;

            TrainingPlan trainingPlan = weekDayTrainingPlan.TrainingPlan;
            if (trainingPlan.WeekDayTrainingPlan.Count <= 1)
            {
                await _repository.DeleteTrainingPlan(trainingPlan);
            }
            else
            {
                trainingPlan.WeekDayTrainingPlan.Remove(weekDayTrainingPlan);
                await _repository.UpdateTrainingPlan(trainingPlan);
            }

            UpdateTrainingPlans(await _repository.GetAllTrainingPlans());
        }

        public async void InitializeViewModel()
        {
            UpdateTrainingPlans(await _repository.GetAllTrainingPlans());

            IsTrainingPlansEmpty = !TrainingPlans.Any();
            TrainingPlans.CollectionChanged += (s, e) => IsTrainingPlansEmpty = !TrainingPlans.Any();
        }

        public void UpdateTrainingPlans(IEnumerable<TrainingPlan> trainingPlans)
        {
            TrainingPlans.Clear();

            foreach (TrainingPlan plan in trainingPlans)
            {
                foreach (TrainingPlanSingleDayModel singleDay in plan.ToSingleDayModels())
                {
                    TrainingPlans.Add(singleDay);
                }
            }

        }

        public async Task UpdateTrainingPlans()
        {
            UpdateTrainingPlans(await _repository.GetAllTrainingPlans());
        }
    }
}

