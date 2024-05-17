using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.VisualElements;
using GymBro.Models.Entities;
using GymBro.Models.Data.EntityFramework.Repositories;
using CommunityToolkit.Mvvm.Input;
using The49.Maui.BottomSheet;
using GymBro.Views.BottomSheets;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;

namespace GymBro.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private Repository _repository;
        private readonly SavedExercisesBottomSheet _bottomSheet;
        public StatisticsViewModel(Repository repository)
        {
            _repository = repository;
            _bottomSheet = new SavedExercisesBottomSheet(this);
        }

        public void LoadData()
        {
            PieChartSeries = new ObservableCollection<ISeries> {
                new PieSeries<int> { Values = new int[] { 2 } },
                new PieSeries<int> { Values = new int[] { 4 } },
                new PieSeries<int> { Values = new int[] { 2 } },
                new PieSeries<int> { Values = new int[] { 10 } },
                new PieSeries<int> { Values = new int[] { 16 } },
                new PieSeries<int> { Values = new int[] { 30 } },
                new PieSeries<int> { Values = new int[] { 2 } }
            };

            BarChartSeries = new ObservableCollection<ISeries> 
            {
                new ColumnSeries<DateTimePoint>
                {
                    Values = new ObservableCollection<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(2021, 1, 1), 50),
                        new DateTimePoint(new DateTime(2021, 1, 2), 55),
                        new DateTimePoint(new DateTime(2021, 1, 2), 55),
                        new DateTimePoint(new DateTime(2021, 1, 2), 45),
                        new DateTimePoint(new DateTime(2021, 1, 3), 60),
                        new DateTimePoint(new DateTime(2021, 1, 4), 64),
                        new DateTimePoint(new DateTime(2021, 1, 5), 70),
                        new DateTimePoint(new DateTime(2021, 1, 6), 50),
                        new DateTimePoint(new DateTime(2021, 1, 7), 75),
                        new DateTimePoint(new DateTime(2021, 2, 8), 75),
                        new DateTimePoint(new DateTime(2021, 2, 9), 85),
                        new DateTimePoint(new DateTime(2021, 2, 10), 80),
                        new DateTimePoint(new DateTime(2021, 2, 11), 94),
                        new DateTimePoint(new DateTime(2021, 2, 11), 100),
                        new DateTimePoint(new DateTime(2021, 1, 11), 50),
                        new DateTimePoint(new DateTime(2021, 1, 12), 55),
                        new DateTimePoint(new DateTime(2021, 1, 13), 55),
                        new DateTimePoint(new DateTime(2021, 1, 14), 45),
                        new DateTimePoint(new DateTime(2021, 1, 15), 60),
                        new DateTimePoint(new DateTime(2021, 1, 16), 64),
                        new DateTimePoint(new DateTime(2021, 1, 17), 70),
                        new DateTimePoint(new DateTime(2021, 1, 18), 50),
                        new DateTimePoint(new DateTime(2021, 2, 19), 75),
                        new DateTimePoint(new DateTime(2021, 2, 20), 75),
                        new DateTimePoint(new DateTime(2021, 2, 21), 85),
                        new DateTimePoint(new DateTime(2021, 3, 22), 80),
                        new DateTimePoint(new DateTime(2021, 2, 22), 94),
                        new DateTimePoint(new DateTime(2021, 1, 30), 100)
                    }
                }
            };

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
        private void AddSavedExercise(Exercise exercise)
        {
            CurrentExercise = exercise;
        }

        [ObservableProperty]
        private Exercise _currentExercise = new Exercise();

        [ObservableProperty]
        private ObservableCollection<Exercise> _savedExercises = new ObservableCollection<Exercise>();

        [ObservableProperty]
        private ObservableCollection<ISeries> _pieChartSeries;

        [ObservableProperty]
        private ObservableCollection<ISeries> _barChartSeries;

        //public Axis[] XAxes { get; set; } =
        //{
        //    new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd.MM"))
        //};



    }
}
