using GymBro.Views;

namespace GymBro
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("CreateTrainingSchedulePage", typeof(CreateTrainingSchedulePage));
        }
    }
}
