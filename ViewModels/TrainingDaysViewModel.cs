using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.DayOfWeekPicker;
using GymBro.Models.Data.EntityFramework.Models;
using GymBro.Models.Data.EntityFramework.Repositories;

namespace GymBro.ViewModels
{
	public partial class TrainingDaysViewModel : ObservableObject
	{

		[ObservableProperty]
		private bool[] _checked = new bool[7];


		private Repository _repository;

		public TrainingDaysViewModel(Repository repository)
		{
			_repository = repository;
        }

		[RelayCommand]
		private void Test()
		{
			string info = "";
			foreach(bool val in _checked)
			{
				info += val.ToString() + "; ";
			}

            AppShell.Current.DisplayAlert("Слюнявчик", info, "Ok");
        }


		[RelayCommand]
		public async void CreateScheduleDay()
		{
			TrainingSchedule trainingSchedule = new TrainingSchedule("Test Schedule");
			await _repository.CreateTrainingSchedule(trainingSchedule);

            ScheduleDay scheduleDay = new ScheduleDay(DayOfWeek.Monday);

			trainingSchedule.ScheduleDays.Add(scheduleDay);
			await _repository.UpdateTrainingSchedule(trainingSchedule);
		}
    }
}

