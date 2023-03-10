using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Practics.GIBDD.App;
using Practics.GIBDD.App.Models;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для LicenseView.xaml
    /// </summary>
    public partial class LicenseView : Window
    {
        private readonly ApplicationContext _db;
        private Drivers _driver;
        private DriverLicenses _license;
        public LicenseView(Guid driverId)
        {
            InitializeComponent();
            _db = new ApplicationContext();

            _driver = _db.Drivers.FirstOrDefault(x => x.Id == driverId);

            if (_driver == null)
            {
                throw new ArgumentException();
            }
            _license = _driver.DriverLicenses.FirstOrDefault();

            DataContext = _license;

            if (_license == null)
            {
                throw new ArgumentException();
            }

            InitializeCategories();
            InitializeDriverInfo();
            InitializeImage();
        }

        private void InitializeCategories()
        {
            var categories = _license.DriverLicenseCategory.ToList();

            foreach (var category in categories)
            {
                var categoryBlock = CreateCategoryBlock(category.DriverLicenseCategories.Code);
                categoriesStackPanel.Children.Add(categoryBlock);
            }

        }

        private void InitializeDriverInfo()
        {
            lastNameRUTextBloxk.Text = _driver.LastName.ToUpper();
            lastNameENTextBlock.Text = _driver.LastName.ToUpper();

            firstNameRUTextBloxk.Text = (_driver.FirstName + " " + _driver.MiddleName).ToUpper();
            firstNameENTextBlock.Text= (_driver.FirstName + " " + _driver.MiddleName).ToUpper();

            licenseDateTextBlock.Text = _license.LicenseDate.ToShortDateString();
            expireDateTextBlock.Text = _license.ExpireDate.ToShortDateString();

            serNumTextBloxk.Text = $"{_license.Series} {_license.Number}";
        }

        private void InitializeImage()
        {
            var image = new BitmapImage();

            using (var mem = new MemoryStream(_driver.Photos.Data))
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

        private Border CreateCategoryBlock(string category)
        {
            var border = new Border()
            {
                BorderThickness=new Thickness(1.5d),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Width=15,
                Height=15,
                Margin = new Thickness(0, 0, 2, 0),
            };
            border.Child = new TextBlock()
            {
                Text = category,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 8,
                FontWeight = FontWeights.Normal,
                FontFamily = new FontFamily("Yu Gothic UI")
            };
            return border;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InitializeDriverInfo();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            InitializeDriverInfo();
        }
    }
}
