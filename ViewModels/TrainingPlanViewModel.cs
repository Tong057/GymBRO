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
using CommunityToolkit.Maui.Alerts;
using GymBro.Views.Popups.Exercise;

namespace GymBro.ViewModels
{
    public partial class TrainingPlanViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;

        public TrainingPlanViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);

            LoadSavedExercises();
            Exercises.CollectionChanged += (s, e) => IsExercisesEmpty = !Exercises.Any();
        }

        private async void LoadSavedExercises()
        {
            IEnumerable<Exercise> savedExercises = await _repository.GetAllExercises();

            foreach (var ex in savedExercises)
            {
                SavedExercises.Add(ex);
            }
        }

        public async Task LoadPlanAsync(int id)
        {
            TrainingPlan = await _repository.GetTrainingPlanById(id);

            foreach (var trainingExercise in TrainingPlan.Exercises.OrderBy(ex => ex.Position))
            {
                Exercises.Add(trainingExercise.Exercise);
            }

            DaysOfWeekCollection.Add(TrainingPlan.Day);
            TrainingPlanTitle = TrainingPlan.Title;
        }

        [ObservableProperty]
        private TrainingPlan _trainingPlan;

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

        [ObservableProperty]
        private bool _isExercisesEmpty = true;

        [RelayCommand]
        private async Task OpenAddExerciseDialog()
        {
            Exercise = new Exercise();

            await Shell.Current.CurrentPage.ShowPopupAsync(new CreateExercisePopup(this));
        }

        [RelayCommand]
        private async Task OpenEditExerciseDialog(Exercise exercise)
        {
            Exercise = exercise.Clone();
            TargetEditExercise = exercise;

            await Shell.Current.CurrentPage.ShowPopupAsync(new EditExercisePopup(this));
        }

        [RelayCommand]
        private async Task OpenSavedExercisesBottomSheet()
        {
            await _bottomSheet.ShowAsync();
        }

        [RelayCommand]
        private void SavedExerciseTapped(Exercise exercise)
        {
            if (!Exercises.Contains(exercise))
                Exercises.Add(exercise);
        }

        [RelayCommand]
        private void AddExercise()
        {
            if (!Exercises.Any(ex => ex.Name == Exercise.Name) && !string.IsNullOrEmpty(Exercise.Name))
            {
                if (SavedExercises.Any(ex => ex.Name == Exercise.Name))
                {
                    Exercises.Add(SavedExercises.FirstOrDefault(ex => ex.Name == Exercise.Name));
                }
                else
                {
                    Exercises.Add(Exercise);
                }
            }

        }

        [RelayCommand]
        private void EditExercise()
        {
            if (!string.IsNullOrEmpty(Exercise.Name))
            {
                TargetEditExercise.Name = Exercise.Name;
                TargetEditExercise.Description = Exercise.Description;
            }
        }

        [RelayCommand]
        public void DeleteExercise(Exercise exercise)
        {
            Exercises.Remove(exercise);
        }

        [RelayCommand]
        public async Task OpenSaveTrainingPlanPopup()
        {
            if (!DaysOfWeekCollection.Any())
            {
                await Toast.Make("Please select at least one day of the week").Show();
            }
            else if (!Exercises.Any())
            {
                await Toast.Make("Please add at least one exercise.").Show();
            }
            else
            {
                await Shell.Current.CurrentPage.ShowPopupAsync(new SaveTrainingPlanPopup(this));
            }
        }

        [RelayCommand]
        public async Task SaveTrainingPlan()
        {
            if (string.IsNullOrEmpty(TrainingPlanTitle))
            {
                await Toast.Make("Please input a plan title").Show();
            }
            else
            {
                if (TrainingPlan != null)
                {
                    TrainingPlan.Title = TrainingPlanTitle;
                    TrainingPlan.Day = DaysOfWeekCollection.First();
                    TrainingPlan.Exercises.Clear();

                    for (int i = 0; i < Exercises.Count; ++i)
                    {
                        TrainingPlan.Exercises.Add(new TrainingPlanExercise(Exercises.ElementAt(i), i));
                    }

                    await _repository.UpdateTrainingPlan(TrainingPlan);
                }
                else
                {
                    foreach (var day in DaysOfWeekCollection)
                    {
                        TrainingPlan plan = new TrainingPlan(TrainingPlanTitle, day);

                        for (int i = 0; i < Exercises.Count; ++i)
                        {
                            plan.Exercises.Add(new TrainingPlanExercise(Exercises.ElementAt(i), i));
                        }

                        await _repository.CreateTrainingPlan(plan);
                    }
                }

                await Shell.Current.Navigation.PopAsync();
            }
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
