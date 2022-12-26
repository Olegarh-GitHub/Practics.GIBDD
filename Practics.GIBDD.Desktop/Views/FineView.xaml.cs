using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Practics.GIBDD.App;
using Practics.GIBDD.App.Models;
using Practics.GIBDD.Desktop.Interfaces;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для FineView.xaml
    /// </summary>
    public partial class FineView : Window
    {
        private readonly int _fineId;
        private readonly ApplicationContext _db;
        private Fines _fine;
        private IRefreshableWindow _parent;


        public FineView(IRefreshableWindow parent, int fineId)
        {
            Closing += Closed;
            
            InitializeComponent();
            _db = new ApplicationContext();

            _fine = _db.Fines.FirstOrDefault(x => x.Id == fineId);
            _parent = parent;
            if (_fine == null)
            {
                throw new ArgumentException();
            }
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            var bindingMap = new Dictionary<TextBox, string>()
            {
                {   carNumTextBox,      "CarNum" },
                {   licenseNumTextBox,  "LicenseNum" },
            };

            foreach (var binding in bindingMap)
            {
                binding.Key.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Source = _fine,
                    Mode = BindingMode.TwoWay,
                    Path = new PropertyPath(binding.Value)
                });
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
            _parent.Refresh();
        }

        private void showImageButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new PhotoView(_fine.PhotoId);
            view.Show();
        }

        private void Closed(object sender, EventArgs args)
        {
            _parent.Refresh();
        }
    }
}
