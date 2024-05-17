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

namespace GymBro.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;
        // Dodaj pola dla obiektów związanych ze statystykami
        private readonly StatisticsManager _statisticsManager;
        private readonly ProgressReporter _progressReporter;
        private readonly CancellationManager _cancellationManager;
        public StatisticsViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);

            _statisticsManager = new StatisticsManager();
            _progressReporter = new ProgressReporter(new Models.Entities.Statistics.ProgressBar());
            _cancellationManager = new CancellationManager();
        }

        public void LoadData()
        {


            BarChartSeries = new ObservableCollection<ISeries>
            {
                new ColumnSeries<StatisticsExerciseStatus>
                {
                    Values = new ObservableCollection<StatisticsExerciseStatus>
                    {
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 60),
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 70),
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 80),
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 90),
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 60),
                        new StatisticsExerciseStatus(new DateTime(2004, 1, 1), 60),
                    }
                }
            };

            PieChartSeries = new ObservableCollection<ISeries>();
            PieChartSeries.Add(new PieSeries<double> { Values = new double[] {1}, Name="wadd" });

        }

        public async Task LoadDataWithProgressAsync(Exercise exercise)
        {
            var cancellationToken = _cancellationManager.GetCancellationToken();

            try
            {
                var exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
                var total = exerciseStatuses.Count;
                var count = 0;

                var statistics = new List<StatisticsExerciseStatus>();

                await Task.Run(async () =>
                {
                    foreach (var exerciseStatus in exerciseStatuses)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        var statisticsExerciseStatus = new StatisticsExerciseStatus(exerciseStatus, exerciseStatus.TrainingDay.StartTime);
                        statistics.Add(statisticsExerciseStatus);
                        // Aktualizuj postęp
                        UpdateProgress(++count, total);
                        await CommunityToolkit.Maui.Alerts.Toast.Make("Toast").Show();
                        _progressReporter.Report($"Obliczenia dla {exerciseStatus.Exercise.Name} zakończone.");
                    }
                }, cancellationToken);

                BarChartSeries = new ObservableCollection<ISeries>
                {
                    new ColumnSeries<StatisticsExerciseStatus>
                    {
                        Values = new ObservableCollection<StatisticsExerciseStatus>(statistics)
                    }
                };
            }
            catch (System.OperationCanceledException)
            {
                // Obsługa anulowania
                _progressReporter.Report("Obliczenia zostały anulowane.");
            }
            catch (Exception ex)
            {
                // Obsługa innych wyjątków
                _progressReporter.Report($"Wystąpił błąd: {ex.Message}");
            }
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
        private async Task OpenSavedExercisesBottomSheet()
        {
            await _bottomSheet.ShowAsync();
        }

        [RelayCommand]
        private async Task AddSavedExercise(Exercise exercise)
        {
            CurrentExercise = exercise;

            await LoadDataWithProgressAsync(exercise);
        }

        [ObservableProperty]
        private Exercise _currentExercise = new Exercise();

        //[ObservableProperty]
        //private static ObservableCollection<StatisticsExerciseStatus> _statisticsExerciseStatuses = new ObservableCollection<StatisticsExerciseStatus>()
        //{
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //                new StatisticsExerciseStatus(new DateOnly(2004, 1, 1), 60),
        //};


        // Metoda do aktualizacji postępu
        private void UpdateProgress(int currentProgress, int total)
        {
            // Aktualizuj wartość postępu
            this.Progress = (currentProgress * 100) / total;
        }

        // Właściwość do śledzenia postępu
        [ObservableProperty]
        private int _progress;

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises = new ObservableCollection<Exercise>();

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieChartSeries = new ObservableCollection<ISeries> {
                new PieSeries<int> { Values = new int[] { 2 }},
                new PieSeries<int> { Values = new int[] { 4 } },
                new PieSeries<int> { Values = new int[] { 2 } },
                new PieSeries<int> { Values = new int[] { 10 } },
                new PieSeries<int> { Values = new int[] { 16 } },
                new PieSeries<int> { Values = new int[] { 30 } },
                new PieSeries<int> { Values = new int[] { 2 } }
            };



[ObservableProperty]
        private ObservableCollection<ISeries> _barChartSeries;

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labels = new string[] {"01.02", "01.02", "01.02", "01.02", "01.02", "01.02", "01.02", }
            }
        };



    }
}
