using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Collections.ObjectModel;
using GymBro.Models.Entities;
using GymBro.Models.Data.EntityFramework.Repositories;
using CommunityToolkit.Mvvm.Input;
using GymBro.Views.BottomSheets;
using GymBro.Models.Entities.Statistics;
using Kotlin.Properties;
using CommunityToolkit.Maui.Alerts;
using Android.OS;
using Android.Widget;
using LiveChartsCore.Defaults;
using System.Threading;

namespace GymBro.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private CancellationTokenSource _cancellationTokenSource;
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;
        public StatisticsViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);
        }

        public async Task LoadData()
        {
            foreach(Exercise exercise in SavedExercises)
            {
                List<ExerciseStatus> exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
                if (!exerciseStatuses.Any())
                    continue;

<<<<<<< HEAD
            PieChartSeries = new ObservableCollection<ISeries>();
            PieChartSeries.Add(new PieSeries<double> { Values = new double[] {1}, Name="wadd" });

        }
=======
                IEnumerable<StatisticsExerciseStatus> statisticsExercises = exerciseStatuses
                    .Select(ex => new StatisticsExerciseStatus(ex, ex.TrainingDay.StartTime));
>>>>>>> f45eb7d (Added progress bar)

                double max = statisticsExercises.MaxBy(ex => ex.WeightedAverageWeight).WeightedAverageWeight;
                double min = statisticsExercises.MinBy(ex => ex.WeightedAverageWeight).WeightedAverageWeight;

                _ratingProgressiveExercises.Add(exercise, max - min);

                
            }

            int counter = 0;
            _ratingProgressiveExercises.OrderByDescending(kvExercise => kvExercise.Value);
            foreach (var kvExercise in _ratingProgressiveExercises)
            {
                if (counter == 7)
                    break;

                PieSeries<double> series = new PieSeries<double>()
                {
                    Values = new double[1] { _ratingProgressiveExercises[kvExercise.Key] },
                    Name = kvExercise.Key.Name
                };
                PieChartSeries.Add(series);
                counter++;
            }
        }

        public async Task LoadDataWithProgressAsync(Exercise exercise, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
            var statistics = new List<StatisticsExerciseStatus>();

            foreach (var exerciseStatus in exerciseStatuses)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var statisticsExerciseStatus = new StatisticsExerciseStatus(exerciseStatus, exerciseStatus.TrainingDay.StartTime);
                statistics.Add(statisticsExerciseStatus);
            }

            int counter = 0;
            ObservableCollection<DateTimePoint> points = new ObservableCollection<DateTimePoint>();
            foreach (StatisticsExerciseStatus statisticsExercise in statistics)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(2000);
                UpdateProgress(++counter, statistics.Count);
                points.Add(new DateTimePoint(statisticsExercise.Date, statisticsExercise.WeightedAverageWeight));
                    
            }

            ColumnSeries<DateTimePoint> columnSeries = new ColumnSeries<DateTimePoint>()
            {
                Values = points
            };
            BarChartSeries.Add(columnSeries);
        }

        public async Task ShowMessage(string text)
        {
            await CommunityToolkit.Maui.Alerts.Toast.Make(text).Show();
        }

        public async void LoadSavedExercises()
        {
            SavedExercises.Clear();
            IEnumerable<Exercise> savedExercises = await _repository.GetAllExercises();

            foreach (var ex in savedExercises)
            {
                SavedExercises.Add(ex);
            }
        }

        [RelayCommand]
        private async Task CancelLoadingTask()
        {
            if (_cancellationTokenSource == null)
                return;

            await _cancellationTokenSource.CancelAsync();
        }

        [RelayCommand]
        private async Task OpenSavedExercisesBottomSheet()
        {
            await _bottomSheet.ShowAsync();
        }

        [RelayCommand]
        private async Task AddSavedExercise(Exercise exercise)
        {
            CurrentExercise = exercise;
            BarChartSeries.Clear();

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                ShowMessage("Starting loading");
                await Task.Run(async () =>
                {
                    await LoadDataWithProgressAsync(exercise, _cancellationTokenSource.Token);
                }, _cancellationTokenSource.Token);
            }
            catch (System.OperationCanceledException)
            {
                ShowMessage("Loading was canceled");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                ShowMessage("An error occurred. Cannot be loaded.");
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            
        }

        private Dictionary<Exercise, double> _ratingProgressiveExercises = new Dictionary<Exercise, double>();

        [ObservableProperty]
        private Exercise _currentExercise = new Exercise();

        private void UpdateProgress(double currentProgress, double total)
        {
            this.Progress = currentProgress / total;
        }

        [ObservableProperty]
        private double _progress;

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises = new ObservableCollection<Exercise>();

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieChartSeries = new ObservableCollection<ISeries>();



        [ObservableProperty]
        private ObservableCollection<ISeries> _barChartSeries = new ObservableCollection<ISeries>();

        [ObservableProperty]
        private List<Axis> _xAxes = new List<Axis>()
        {
            new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("MM.dd"))
        };



    }
}
