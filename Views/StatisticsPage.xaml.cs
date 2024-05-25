using GymBro.ViewModels;
using SkiaSharp;

namespace GymBro.Views;

public partial class StatisticsPage : ContentPage
{
    private StatisticsViewModel _statisticsVM;
    public StatisticsPage(StatisticsViewModel statisticsVM)
    {
        InitializeComponent();

        BindingContext = statisticsVM;
        _statisticsVM = statisticsVM;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        _statisticsVM.LoadSavedExercises();
        await _statisticsVM.LoadPieDiagram();
    }

}