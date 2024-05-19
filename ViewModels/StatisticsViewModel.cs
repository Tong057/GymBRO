using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System.Collections.ObjectModel;
using GymBro.Models.Entities;
using GymBro.Models.Data.EntityFramework.Repositories;
using CommunityToolkit.Mvvm.Input;
using GymBro.Views.BottomSheets;
using GymBro.Models.Entities.Statistics;
using LiveChartsCore.SkiaSharpView.Painting;

namespace GymBro.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;
        private Dictionary<Exercise, double> _ratingProgressiveExercises = new Dictionary<Exercise, double>();

        public StatisticsViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);
        }

        public async Task LoadPieDiagram()
        {
            foreach (Exercise exercise in SavedExercises)
            {
                List<ExerciseStatus> exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
                if (!exerciseStatuses.Any())
                    continue;

                IEnumerable<StatisticsExerciseStatus> statisticsExercises = exerciseStatuses
                    .Select(ex => new StatisticsExerciseStatus(ex, ex.TrainingDay.StartTime));

                double max = statisticsExercises.Max(ex => ex.WeightedAverageWeight);
                double min = statisticsExercises.Min(ex => ex.WeightedAverageWeight);

                _ratingProgressiveExercises.Add(exercise, max - min);
            }

            int counter = 0;
            var orderedExercises = _ratingProgressiveExercises.OrderByDescending(kv => kv.Value);
            PieChartSeries = new ObservableCollection<ISeries>();

            foreach (var kvExercise in orderedExercises)
            {
                if (counter == 7)
                    break;

                PieSeries<double> series = new PieSeries<double>
                {
                    Values = new double[1] { kvExercise.Value },
                    Name = kvExercise.Key.Name
                };
                PieChartSeries.Add(series);
                counter++;
            }
        }

        public async Task LoadDataWithProgressAsync(Exercise exercise)
        {
            var exerciseStatuses = await _repository.GetExerciseStatusesByExercise(exercise);
            var statistics = new List<StatisticsExerciseStatus>();

            foreach (var exerciseStatus in exerciseStatuses)
            {
                var statisticsExerciseStatus = new StatisticsExerciseStatus(exerciseStatus, exerciseStatus.TrainingDay.StartTime);
                statistics.Add(statisticsExerciseStatus);
            }

            ObservableCollection<double> values = new ObservableCollection<double>();
            List<string> labels = new List<string>();

            foreach (StatisticsExerciseStatus statisticsExercise in statistics)
            {
                values.Add(statisticsExercise.WeightedAverageWeight);
                labels.Add(statisticsExercise.Date.ToString("MM.dd"));
            }

            ColumnSeries<double> columnSeries = new ColumnSeries<double>
            {
                Values = values
            };

            XAxes[0].Labels = labels;
            BarChartSeries.Add(columnSeries);
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
            BarChartSeries.Clear();

            try
            {
                await Task.Run(async () =>
                {
                    await LoadDataWithProgressAsync(exercise);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        [ObservableProperty]
        private Exercise _currentExercise;

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises;

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieChartSeries = new ObservableCollection<ISeries>();

        [ObservableProperty]
        private ObservableCollection<ISeries> _barChartSeries = new ObservableCollection<ISeries>();

        [ObservableProperty]
        private List<Axis> _xAxes = new List<Axis>()
        {
            new Axis
            {
                LabelsRotation = 15,
                SeparatorsPaint = new SolidColorPaint { Color = SkiaSharp.SKColors.Gray },
                Labels = new List<string>()
            }
        };

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
