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
		private ObservableCollection<DayOfWeek> _checked = new ObservableCollection<DayOfWeek>();


		private Repository _repository;

		public TrainingDaysViewModel(Repository repository)
		{
			_repository = repository;
        }

		[RelayCommand]
		private void Test()
		{
			string info = "";
			foreach(DayOfWeek val in Checked)
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

		[RelayCommand]
		public void SetAllChecked()
		{
			Checked.Add(DayOfWeek.Monday);
            Checked.Add(DayOfWeek.Tuesday);
            Checked.Add(DayOfWeek.Wednesday);
            Checked.Add(DayOfWeek.Thursday);
            Checked.Add(DayOfWeek.Friday);
            Checked.Add(DayOfWeek.Saturday);
            Checked.Add(DayOfWeek.Sunday);
        }
    }
}

