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
    public partial class FineListView : Window, IRefreshableWindow
    {
        private readonly ApplicationContext _context;
        
        public FineListView()
        {
            InitializeComponent();

            _context = new ApplicationContext();

            Refresh();
        }

        private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = searchTextBox.Text;

            var items = _context.Fines.ToList().Where(item =>
                item.CarNum.ToLower().Contains(text) || item.CreateDate.ToString("dd.MM.yyyy").Contains(text)).ToList();

            fineListView.ItemsSource = items;
        }

        private void ReadButton_OnClick(object sender, RoutedEventArgs e)
        {
            Fines chosen = fineListView.SelectedItem as Fines;

            if (chosen == null)
            {
                MessageBox.Show("Пожалуйста, выберите штраф из списка", "Просьба", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }
            
            new FineView(this, chosen.Id).Show();
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            Fines chosen = fineListView.SelectedItem as Fines;

            if (chosen == null)
            {
                MessageBox.Show("Пожалуйста, выберите штраф из списка", "Просьба", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return;
            }

            MessageBoxResult result = MessageBox.Show("Уверены?", "Предупреждение", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.No)
                return;

            _context.Fines.Remove(chosen);
            _context.SaveChanges();

            Refresh();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new ChooseNextView().Show();
            
            Close();
        }

        private void FsspButton_OnClick(object sender, RoutedEventArgs e)
        {
            new ExportFSSPView().Show();
            
        }

        public void Refresh()
        {
            fineListView.ItemsSource = new ApplicationContext().Fines.ToList();
        }
    }
}