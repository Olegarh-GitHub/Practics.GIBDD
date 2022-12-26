using System;
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
    /// Логика взаимодействия для DriversListView.xaml
    /// </summary>
    public partial class DriversListView : Window, IRefreshableWindow
    {
        private readonly ApplicationContext _db;
        public DriversListView()
        {
            InitializeComponent();
            _db = new ApplicationContext();

            driversListView.ItemsSource = _db.Drivers.ToList();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new DriverView(Guid.NewGuid(), this, true);
            
            view.Show();
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            var driver = driversListView.SelectedItem as Drivers;

            var view = new DriverView(driver.Id, this);
            view.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Drivers chosen = driversListView.SelectedItem as Drivers;

            if (chosen == null)
            {
                MessageBox.Show("Пожалуйста, выберите водителя из списка", "Просьба", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }

            MessageBoxResult result = MessageBox.Show("Уверены?", "Предупреждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.No)
                return;

            _db.Entry(chosen).State = EntityState.Deleted;
            _db.Drivers.Remove(chosen);
            _db.SaveChanges();

            Refresh();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new ChooseNextView().Show();
            
            Close();
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchTextBox.Text.ToLower();

            List<Drivers> searchedDrivers = _db.Drivers.Where
            (
                driver => driver.Email.ToLower().Contains(text) ||
                          driver.LastName.ToLower().Contains(text) ||
                          driver.FirstName.ToLower().Contains(text) ||
                          driver.LastName.ToLower().Contains(text)
            ).ToList();

            driversListView.ItemsSource = searchedDrivers;
        }

        public void Refresh()
        {
            driversListView.ItemsSource = new ApplicationContext().Drivers.ToList();
        }
    }
}
