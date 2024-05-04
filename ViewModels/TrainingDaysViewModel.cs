using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.DayOfWeekPicker;
using GymBro.Models.Data.EntityFramework.Models;
using GymBro.Models.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GymBro.ViewModels
{
	public partial class TrainingDaysViewModel : ObservableObject
	{

		[ObservableProperty]
		private ObservableHashSet<DayOfWeek> _checked = new ObservableHashSet<DayOfWeek>();


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
			//Create Schedule
			TrainingSchedule trainingSchedule = new TrainingSchedule("Test Schedule");
			await _repository.CreateTrainingSchedule(trainingSchedule);

            ScheduleDay scheduleDay = new ScheduleDay(DayOfWeek.Monday);
			trainingSchedule.ScheduleDays.Add(scheduleDay);

			TrainingScheduleExercises trainingScheduleExercises = new TrainingScheduleExercises();
			trainingSchedule.TrainingScheduleExercises = trainingScheduleExercises;

			trainingScheduleExercises.Exercises.Add(new Exercise("Pull-Up", "Do PullUps"));
            trainingScheduleExercises.Exercises.Add(new Exercise("Drops", "Do Drops"));
            trainingScheduleExercises.Exercises.Add(new Exercise("Pelmeski", "Eat Pelmeni"));
            await _repository.UpdateTrainingSchedule(trainingSchedule);




			//Create Training
			Training training = new Training(trainingSchedule);
			training.StartTime = DateTime.Now;

			ExerciseStatus exerciseStatus = new ExerciseStatus(trainingSchedule.TrainingScheduleExercises.Exercises.ElementAt(0));
			exerciseStatus.ExerciseWeights.Add(new ExerciseWeight(10));

            training.ExerciseStatuses.Add(exerciseStatus);
			await _repository.CreateTraining(training);


			AppShell.Current.DisplayAlert("Alert",
				$"{training.StartTime} | Amount: {training.ExerciseStatuses.ElementAt(0).Exercise.Name}", "OK");
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

