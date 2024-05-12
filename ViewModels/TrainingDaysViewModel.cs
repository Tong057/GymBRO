using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
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
		public async Task DeleteScheduleDay(int scheduleDayId)
		{
			ScheduleDay scheduleDay = await _repository.GetScheduleDayById(scheduleDayId);
			if (scheduleDay == null)
				return;

			TrainingSchedule trainingSchedule = scheduleDay.TrainingSchedule;
			if(trainingSchedule.ScheduleDays.Count <= 1)
			{
				await _repository.DeleteTrainingSchedule(trainingSchedule);
			} else
			{
				trainingSchedule.ScheduleDays.Remove(scheduleDay);
				await _repository.UpdateTrainingSchedule(trainingSchedule);
            }

			UpdateTrainingSchedules(await _repository.GetAllTrainingSchedules());
		}

		[RelayCommand]
		public async Task Test()
		{
			TrainingSchedule schedule = new TrainingSchedule("Test");
			schedule.ScheduleDays.Add(new ScheduleDay(DayOfWeek.Friday));
			schedule.TrainingScheduleExercises.Exercises.Add(new Exercise("TestName", "TestDesc"));

			await _repository.CreateTrainingSchedule(schedule);

			ScheduleDay t1 = new ScheduleDay(DayOfWeek.Friday);
			ScheduleDay t2 = new ScheduleDay(DayOfWeek.Monday);

			TrainingSchedule schedule2 = new TrainingSchedule("DoubleTest");
			schedule2.ScheduleDays.Add(t1);
			schedule2.ScheduleDays.Add(t2);
			schedule2.TrainingScheduleExercises.Exercises.Add(new Exercise("DoubleTestName", "DoubleTestDesc"));
			schedule2.TrainingScheduleExercises.Exercises.Add(new Exercise("DoubleTestName2", "DoubleTestDesc2"));


            await _repository.CreateTrainingSchedule(schedule2);

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

