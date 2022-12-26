
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Practics.GIBDD.App;
using Practics.GIBDD.App.Models;
using Practics.GIBDD.Desktop.Interfaces;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для DriverView.xaml
    /// </summary>
    public partial class DriverView : Window
    {
        public bool _create { get; set; }
        private readonly ApplicationContext _db;
        private Drivers _driver;
        private IRefreshableWindow _parent;
        public DriverView(Guid driverGuid, IRefreshableWindow parent, bool create = false)
        {
            InitializeComponent();
            _db = new ApplicationContext();

            _driver = _db.Drivers.FirstOrDefault(x => x.Id == driverGuid) ?? new Drivers() {Id = driverGuid};
            if (_driver == null && !create)
            {
                throw new ArgumentException();
            }
            
            DataContext = _driver;
            _parent = parent;
            _create = create;
            InitializeBindings();

            Title = _driver.FullName;
        }

        private void InitializeBindings()
        {
            var bindingMap = new Dictionary<TextBox, string>()
            {
                {   driverLastNameTextBox,     "LastName" },
                {   driverFirstNameTextBox,    "FirstName" },
                {   driverMiddleNameTextBox,   "MiddleName" },
                {   driverPassportTextBox,     "Passport" },
                {   driverRegAddressTextBox,   "RegAddress" },
                {   driverLiveAddressTextBox,  "LiveAddress" },
                {   driverPlaceOfWorkTextBox,  "PlaceOfWork" },
                {   driverWorkPositionTextBox, "WorkPosition" },
                {   driverEmailTextBox,        "Email" },
                {   driverRemarksTextBox,      "Remarks" },
            };

            foreach (var binding in bindingMap)
            {
                binding.Key.SetBinding(TextBox.TextProperty, new Binding()
                {
                    Source = _driver,
                    Mode = BindingMode.TwoWay,
                    Path = new PropertyPath(binding.Value)
                });
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
        }

        private void AddOrUpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                EntityState state = _create ? EntityState.Added : EntityState.Modified;

                if (state == EntityState.Added)
                {
                    _db.Drivers.Add(_driver);
                }

                _db.Entry(_driver).State = state;
                _db.SaveChanges();
                _parent.Refresh();

                MessageBox.Show("Успешно!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Проверьте корректность введенных данных");
            }
            
            
        }

        private void CheckDriverLicense_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LicenseView view = new LicenseView(_driver.Id);
            
                view.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show("У водителя нет удостоверения");
            }
        }

        private void DriverImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == null) 
                return;
            
            FileInfo fileInfo = new FileInfo(openDialog.FileName);

            FileStream fileStream = fileInfo.OpenRead();

            fileStream.Seek(0, SeekOrigin.Begin);
            MemoryStream memoryStream = new MemoryStream();

            memoryStream.Seek(0, SeekOrigin.Begin);
            fileStream.CopyTo(memoryStream);

            byte[] data = memoryStream.ToArray();

            var ph = new Photos() {Data = data, Name = "photo", DateCreated = DateTime.Now};
            _driver.Photos = ph;
            _db.Photos.Add(ph);
            _db.SaveChanges();
            var image = new BitmapImage();

            using (var mem = new MemoryStream(data))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();

            driverImage.Source = image;
        }
    }
}
