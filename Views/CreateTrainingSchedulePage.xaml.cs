using GymBro.ViewModels;

namespace GymBro.Views;

public partial class CreateTrainingSchedulePage : ContentPage
{
	CreateTrainingScheduleViewModel _createTrainingScheduleVM;
	public CreateTrainingSchedulePage(CreateTrainingScheduleViewModel createTrainingScheduleVM)
	{
		InitializeComponent();
        BindingContext = createTrainingScheduleVM;
		_createTrainingScheduleVM = createTrainingScheduleVM;
	}
}