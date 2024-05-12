using GymBro.Views;

namespace GymBro
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("CreateTrainingPlanPage", typeof(CreateTrainingPlanPage));
            Routing.RegisterRoute("TrainingDayPage", typeof(TrainingDayPage));
        }
    }
}
