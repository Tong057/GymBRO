using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.DayOfWeekPicker;
using GymBro.Models.Data.EntityFramework.Repositories;

namespace GymBro.ViewModels
{
	public partial class TrainingDaysViewModel : ObservableObject
	{
		public TrainingDaysViewModel(Repository repository)
		{
            SelectedDays = new ObservableCollection<string>();
            SelectedDays.CollectionChanged += (s, e) =>
            {
                AppShell.Current.DisplayAlert("TETE", "wadad", "OK");
            };
        }

        [ObservableProperty]
        private ObservableCollection<string> _selectedDays;

        [RelayCommand]
		private void ShowSelectedDays()
		{
            foreach (var day in SelectedDays)
                AppShell.Current.DisplayAlert("TETE", day, "OK");
        }
    }
}

