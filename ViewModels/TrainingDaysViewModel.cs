using System;
using CommunityToolkit.Mvvm.ComponentModel;
using GymBro.Models.Data.EntityFramework.Repositories;

namespace GymBro.ViewModels
{
	public partial class TrainingDaysViewModel : ObservableObject
	{
		public TrainingDaysViewModel(Repository repository)
		{
			//AppShell.Current.DisplayAlert("TETE", (repository == null).ToString(), "OK");
		}
	}
}

