using System.Collections.ObjectModel;
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
		public async Task DeleteWeekDayTrainingPlan(int weekDayTrainingPlanId)
		{
			WeekDayTrainingPlan weekDayTrainingPlan = await _repository.GetWeekDayTrainingPlanById(weekDayTrainingPlanId);
			if (weekDayTrainingPlan == null)
				return;

			TrainingPlan trainingPlan = weekDayTrainingPlan.TrainingPlan;
			if(trainingPlan.WeekDayTrainingPlan.Count <= 1)
			{
				await _repository.DeleteTrainingPlan(trainingPlan);
			} else
			{
                trainingPlan.WeekDayTrainingPlan.Remove(weekDayTrainingPlan);
				await _repository.UpdateTrainingPlan(trainingPlan);
            }

            UpdateTrainingPlans(await _repository.GetAllTrainingPlans());
		}

		[RelayCommand]
		public async Task Test()
		{
			TrainingPlan plan = new TrainingPlan("Test");
            plan.WeekDayTrainingPlan.Add(new WeekDayTrainingPlan(DayOfWeek.Friday));
            plan.TrainingPlanExercises.Exercises.Add(new Exercise("TestName", "TestDesc"));

			await _repository.CreateTrainingPlan(plan);

			WeekDayTrainingPlan t1 = new WeekDayTrainingPlan(DayOfWeek.Friday);
			WeekDayTrainingPlan t2 = new WeekDayTrainingPlan(DayOfWeek.Monday);

			TrainingPlan plan2 = new TrainingPlan("DoubleTest");
            plan2.WeekDayTrainingPlan.Add(t1);
            plan2.WeekDayTrainingPlan.Add(t2);
            plan2.TrainingPlanExercises.Exercises.Add(new Exercise("DoubleTestName", "DoubleTestDesc"));
            plan2.TrainingPlanExercises.Exercises.Add(new Exercise("DoubleTestName2", "DoubleTestDesc2"));


            await _repository.CreateTrainingPlan(plan2);

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
			foreach(TrainingPlan plan in trainingPlans)
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

