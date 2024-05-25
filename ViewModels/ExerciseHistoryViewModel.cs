using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Controls.Popups;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.ViewModels.Popups;
using GymBro.Views.BottomSheets;
using System.Collections.ObjectModel;

namespace GymBro.ViewModels
{
    public partial class ExerciseHistoryViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;

        public ExerciseHistoryViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);
        }

        [RelayCommand]
        private async Task OpenSavedExercisesBottomSheet()
        {
            await _bottomSheet.ShowAsync();
        }

        [RelayCommand]
        private async Task SavedExerciseTapped(Exercise exercise)
        {
            CurrentExercise = exercise;

            await LoadEntries(exercise);
        }

        private async Task LoadEntries(Exercise exercise)
        {
            Entries.Clear();

            var exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
            foreach (var exerciseStatus in exerciseStatuses)
            {
                Entries.Add(exerciseStatus);
            }
        }

        [RelayCommand]
        private async Task DeleteEntry(ExerciseStatus entry)
        {
            Entries.Remove(entry);

            await _repository.DeleteExerciseStatus(entry);
        }

        [RelayCommand]
        public async Task ShowExerciseTrainingInformationPopup(ExerciseStatus entry)
        {
            CompletedDataExerciseInfoPopupViewModel popupViewModel = new CompletedDataExerciseInfoPopupViewModel(CurrentExercise, entry);
            var popup = new CompletedDataExerciseInfoPopup(popupViewModel);

            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        [ObservableProperty]
        private Exercise _currentExercise = new Exercise();

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises;

        [ObservableProperty]
        private ObservableCollection<ExerciseStatus> _entries = new ObservableCollection<ExerciseStatus>();

        public async void LoadSavedExercises()
        {
            SavedExercises = new ObservableCollection<Exercise>();

            foreach (var ex in await _repository.GetAllExercises())
            {
                SavedExercises.Add(ex);
            }
        }

    }
}
