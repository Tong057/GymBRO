using GymBro.Controls.NavigationBar;
using GymBro.ViewModels;
using System.Runtime.CompilerServices;

namespace GymBro.Views.ContentViews;

public partial class TrainingPlanContentView : ContentView
{
	public TrainingPlanContentView()
	{
		InitializeComponent();  
	}

    public bool IsForAdd { get; set; }
    public bool IsForEdit { get; set; }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (IsForAdd && !IsForEdit)
        {
            NavBar.Title = (string)Application.Current.Resources["CreateTrainingPlan"];
            //NavBar.SetBinding(CustomNavigationBar.RightButtonCommandProperty, "OpenSaveTrainingPlanPopupCommand");
        }
        else if (!IsForAdd && IsForEdit)
        {
            NavBar.Title = (string)Application.Current.Resources["EditTrainingPlan"];
            //NavBar.SetBinding(CustomNavigationBar.RightButtonCommandProperty, "OpenUpdateTrainingPlanPopupCommand");
        }
    }
}