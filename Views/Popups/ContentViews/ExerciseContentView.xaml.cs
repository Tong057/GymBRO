using System.Runtime.CompilerServices;

namespace GymBro.Views.Popups.ContentViews;

public partial class ExerciseContentView : ContentView
{
	public ExerciseContentView()
	{
		InitializeComponent();
	}

    public event EventHandler CloseRequested;

    public bool IsForAdd { get; set; }
    public bool IsForEdit { get; set; }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if (IsForAdd && !IsForEdit)
        {
            TitleLabel.Text = (string)Application.Current.Resources["CreateExercise"];
            SaveButton.SetBinding(Button.CommandProperty, "AddExerciseCommand");
            SelectButton.SetBinding(Button.CommandProperty, "OpenSavedExercisesBottomSheetCommand");
        }
        else if (!IsForAdd && IsForEdit)
        {
            TitleLabel.Text = (string)Application.Current.Resources["EditExercise"];
            SaveButton.SetBinding(Button.CommandProperty, "EditExerciseCommand");
            SelectButton.IsVisible = false;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }
}