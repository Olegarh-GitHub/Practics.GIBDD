using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Practics.GIBDD.App;
using Practics.GIBDD.App.Models;
using Practics.GIBDD.Desktop.Interfaces;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для VehiclesListView.xaml
    /// </summary>
    public partial class VehiclesListView : Window, IRefreshableWindow
    {
        private readonly ApplicationContext _db;
        public List<Vehicles> Vehicles { get; set; }

        public VehiclesListView()
        {
            InitializeComponent();

            _db = new ApplicationContext();

            _db.Vehicles.Load();

            Vehicles = _db.Vehicles.ToList();

            vehiclesListView.ItemsSource = Vehicles;

        }

        private Vehicles GetSelectedVehicle()
        {
            var vehilce = vehiclesListView.SelectedItem as Vehicles;
            return vehilce;
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new RegNumberView(this, 0, true);
            
            view.Show();
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var vehicle = GetSelectedVehicle();
            if (vehicle == null)
            {
                MessageBox.Show("Пожалуйста, выберите ТС из списка", "Просьба", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }
            var view = new RegNumberView(this, vehicle.Id);
            view.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicles chosen = GetSelectedVehicle();

            if (chosen == null)
            {
                MessageBox.Show("Пожалуйста, выберите ТС из списка", "Просьба", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }

            MessageBoxResult result = MessageBox.Show("Уверены?", "Предупреждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.No)
                return;

            _db.Entry(chosen).State = EntityState.Deleted;
            _db.Vehicles.Remove(chosen);
            _db.SaveChanges();

            Refresh();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new ChooseNextView().Show();
            
            Close();
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchTextBox.Text.ToLower();

            var found = _db.Vehicles
                .ToList()
                .Where(vehicle => vehicle.Model.ToLower().Contains(text) ||
                                  vehicle.Manufacturer.ToLower().Contains(text) ||
                                  vehicle.Year.ToString().Contains(text) || vehicle.VINCode.ToLower().Contains(text))
                .ToList();

            vehiclesListView.ItemsSource = found;
        }

        public void Refresh()
        {
            vehiclesListView.ItemsSource = new ApplicationContext().Vehicles.ToList();
        }
    }
}
