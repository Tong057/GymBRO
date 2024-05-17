using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.Popups;
using GymBro.Models.Entities;
using GymBro.Models.Events;

namespace GymBro.ViewModels.Popups
{
	public partial class CompletedDataExercisePopupViewModel : ObservableObject
	{
        public CompletedDataExercisePopup Popup;

        [ObservableProperty]
        private Exercise _exercise;

        public event EventHandler<CompletedDataExerciseArgs> ExerciseDataCollected;

		[ObservableProperty]
        private ObservableCollection<ExerciseWeight> _weightCollection;


        [ObservableProperty]
        private string _weightEntryText = "";

        [ObservableProperty]
        private string _repeatsEntryText = "";

        [ObservableProperty]
        private string _noteEditorText = "";

        [ObservableProperty]
        private string _setsEntryText = "";

        [ObservableProperty]
        private bool _advancedLayout = false;

        public CompletedDataExercisePopupViewModel(Exercise exercise)
		{
			Exercise = exercise;
            WeightCollection = new ObservableCollection<ExerciseWeight>()
            {
                new ExerciseWeight()
            };
		}

        protected void OnExerciseDataCollected(ExerciseStatus exerciseStatus)
        {
            ExerciseDataCollected?.Invoke(this, new CompletedDataExerciseArgs(exerciseStatus));
        }

        [RelayCommand]
        public void DoneButton()
        {
            Note note = string.IsNullOrEmpty(NoteEditorText.Trim()) ? null : new Note(NoteEditorText.Trim());
            ExerciseStatus exerciseStatus = new ExerciseStatus(_exercise, note);

            if (!AdvancedLayout)
            {
                if (string.IsNullOrEmpty(WeightEntryText) || string.IsNullOrEmpty(RepeatsEntryText))
                    return;

                if (!double.TryParse(WeightEntryText, out double weights))
                    return;
                if (!int.TryParse(SetsEntryText, out int sets))
                    return;
                if (!int.TryParse(RepeatsEntryText, out int repeats))
                    return;

                for (int i = 0; i < sets; i++)
                {
                    exerciseStatus.ExerciseWeights.Add(new ExerciseWeight(weights, repeats));
                }
            } else
            {
                if (!WeightCollection.Where(weight => weight != null).Any())
                    return;

                foreach(ExerciseWeight exerciseWeight in WeightCollection)
                {
                    if (exerciseWeight == null)
                        continue;

                    exerciseStatus.ExerciseWeights.Add(exerciseWeight);
                }
            }
            

            OnExerciseDataCollected(exerciseStatus);
            Popup.Close();
        }

        [RelayCommand]
        private void AddWeightRow()
        {
            WeightCollection.Add(new ExerciseWeight());
        }

        [RelayCommand]
        private void ChangeLayout()
        {
            AdvancedLayout = !AdvancedLayout;
        }
    }

    
}

