using System.Windows;

namespace Practics.GIBDD.Desktop.Views
{
    public partial class ChooseNextView : Window
    {
        public ChooseNextView()
        {
            InitializeComponent();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AuthorizationView().Show();
            
            Close();
        }

        private void CheckVehiclesButton_OnClick(object sender, RoutedEventArgs e)
        {
            new VehiclesListView().Show();
            
            Close();
        }

        private void CheckDriversButton_OnClick(object sender, RoutedEventArgs e)
        {
            new DriversListView().Show();
            
            Close();
        }

        private void CheckFinesButton_OnClick(object sender, RoutedEventArgs e)
        {
            new FineListView().Show();
            
            Close();
        }
    }
}