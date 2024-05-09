using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models;
using GymBro.Models.Data.EntityFramework.Models;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Views;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GymBro.ViewModels
{
	public partial class TrainingDaysViewModel : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<TrainingScheduleSingleDayModel> _trainingSchedules = new ObservableCollection<TrainingScheduleSingleDayModel>();

		[ObservableProperty]
		private bool _isTrainingSchedulesEmpty = false;


		private Repository _repository;

		public TrainingDaysViewModel(Repository repository)
		{
			_repository = repository;

			InitializeViewModel();
        }

		[RelayCommand]
		public async Task OpenCreateTrainingSchedule()
		{
            await Shell.Current.GoToAsync(nameof(CreateTrainingSchedulePage));
        }

		[RelayCommand]
		public async Task Test()
		{
			TrainingSchedule schedule = new TrainingSchedule("Test");
			schedule.ScheduleDays.Add(new ScheduleDay(DayOfWeek.Friday));

			await _repository.CreateTrainingSchedule(schedule);

            UpdateTrainingSchedules(await _repository.GetAllTrainingSchedules());
        }


		public async void InitializeViewModel()
		{
            UpdateTrainingSchedules(await _repository.GetAllTrainingSchedules());

			IsTrainingSchedulesEmpty = !TrainingSchedules.Any();
            TrainingSchedules.CollectionChanged += (s, e) => IsTrainingSchedulesEmpty = !TrainingSchedules.Any();
        }

		public void UpdateTrainingSchedules(IEnumerable<TrainingSchedule> trainingSchedules)
		{
            TrainingSchedules.Clear();
			foreach(TrainingSchedule schedule in trainingSchedules)
			{
                foreach (TrainingScheduleSingleDayModel singleDay in schedule.ToSingleDayModels())
				{ 
					TrainingSchedules.Add(singleDay);
                }
            }
		}
    }
}

