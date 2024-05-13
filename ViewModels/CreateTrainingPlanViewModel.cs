using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymBro.Views.Popups;
using GymBro.Models.Data.EntityFramework.Repositories;
using GymBro.Models.Entities;
using GymBro.Views.BottomSheets;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace GymBro.ViewModels
{
    public partial class CreateTrainingPlanViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;
        private SaveTrainingPlanPopup _savePlanPopup;

        public CreateTrainingPlanViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);

            LoadSavedExercises();
        }

        private async void LoadSavedExercises()
        {
            IEnumerable<Exercise> savedExercises = await _repository.GetAllExercises();

            foreach (var ex in savedExercises)
            {
                SavedExercises.Add(ex);
            }
        }

        [ObservableProperty]
        private string _trainingPlanTitle;

        [ObservableProperty]
        private Exercise _exercise;

        [ObservableProperty]
        private Exercise _targetEditExercise;

        [ObservableProperty]
        private ObservableCollection<Exercise> _exercises = new ObservableCollection<Exercise>();

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises = new ObservableCollection<Exercise>();

        [ObservableProperty]
        private ObservableHashSet<DayOfWeek> _daysOfWeekCollection = new ObservableHashSet<DayOfWeek>();

        [RelayCommand]
        private async Task OpenAddExerciseDialog()
        {
            Exercise = new Exercise();

            var createExercisePopup = new CreateExercisePopup(this);
            await Shell.Current.CurrentPage.ShowPopupAsync(createExercisePopup);
        }

        [RelayCommand]
        private async Task OpenEditExerciseDialog(Exercise exercise)
        {
            Exercise = exercise.Clone();
            TargetEditExercise = exercise;

            var editExercisePopup = new EditExercisePopup(this);
            await Shell.Current.CurrentPage.ShowPopupAsync(editExercisePopup);
        }

        [RelayCommand]
        private async Task OpenSavedExercisesBottomSheet()
        {
            await _bottomSheet.ShowAsync();
        }

        [RelayCommand]
        private void AddSavedExercise(Exercise exercise)
        {
            if (!Exercises.Contains(exercise))
                Exercises.Add(exercise);
        }

        [RelayCommand]
        private void AddExercise()
        {
            if (!Exercises.Contains(Exercise))
                Exercises.Add(Exercise);
        }

        [RelayCommand]
        private void EditExercise()
        {
            TargetEditExercise.Name = Exercise.Name;
            TargetEditExercise.Description = Exercise.Description;
        }

        [RelayCommand]
        public void DeleteExercise(Exercise exercise)
        {
            Exercises.Remove(exercise);
        }

        [RelayCommand]
        public async Task OpenSaveTrainingPlanPopup()
        {
            _savePlanPopup = new SaveTrainingPlanPopup(this);
            await Shell.Current.CurrentPage.ShowPopupAsync(_savePlanPopup);
        }

        [RelayCommand]
        public async Task SaveTrainingPlan()
        {
            TrainingPlan plan = new TrainingPlan(TrainingPlanTitle);

            foreach (var day in DaysOfWeekCollection)
            {
                plan.WeekDayTrainingPlan.Add(new WeekDayTrainingPlan(day));
            }

            foreach (var ex in Exercises)
            {
                plan.TrainingPlanExercises.Exercises.Add(ex);
            }

            await _repository.CreateTrainingPlan(plan);
            await Shell.Current.Navigation.PopAsync(true);
        }

        #region Drag and drop section

        private Exercise _itemBeingDragged;

        [RelayCommand]
        public void ItemDragged(Exercise exercise)
        {
            exercise.IsBeingDragged = true;
            _itemBeingDragged = exercise;
        }
        [RelayCommand]
        public void ItemDragLeave(Exercise exercise)
        {
            exercise.IsBeingDraggedOver = false;
        }
        [RelayCommand]
        public void ItemDraggedOver(Exercise exercise)
        {
            if (exercise == _itemBeingDragged)
            {
                exercise.IsBeingDragged = false;
            }
            exercise.IsBeingDraggedOver = exercise != _itemBeingDragged;
        }
        [RelayCommand]
        public void ItemDropped(Exercise exercise)
        {
            try
            {
                var itemToMove = _itemBeingDragged;
                var itemToInsertBefore = exercise;
                if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                    return;
                int insertAtIndex = Exercises.IndexOf(itemToInsertBefore);
                if (insertAtIndex >= 0 && insertAtIndex < Exercises.Count)
                {
                    Exercises.Remove(itemToMove);
                    Exercises.Insert(insertAtIndex, itemToMove);
                    itemToMove.IsBeingDragged = false;
                    itemToInsertBefore.IsBeingDraggedOver = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
