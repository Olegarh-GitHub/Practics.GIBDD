using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using System.Windows.Controls;
using Practics.GIBDD.App;
using Practics.GIBDD.App.Models;
using Practics.GIBDD.Desktop.Interfaces;
using Practics.GIBDD.RegistrationNumbers.Models;
using Practics.GIBDD.RegistrationNumbers.Services;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateRegNumberView.xaml
    /// </summary>
    /// 
    public static class ImageExtensions
    {
        public static ImageSource ToImageSource(this Bitmap image)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
    public partial class RegNumberView : Window
    {
        private readonly ApplicationContext _db;
        private readonly Vehicles _vehicle;
        private RegNumbers _regNumber;
        private IRefreshableWindow _parent;
        private bool _isCreate;
        public RegNumberView(IRefreshableWindow parent, int vehiceId, bool isCreate = false)
        {
            _parent = parent;
            _isCreate = isCreate;
            
            InitializeComponent();

            _db = new ApplicationContext();
            _vehicle = _db.Vehicles.FirstOrDefault(x => x.Id == vehiceId) ?? new Vehicles();
            _regNumber = _vehicle.RegNumbers?.FirstOrDefault() ?? new RegNumbers()
            {
                Number = 58, DateCreated = DateTime.Now,
                Series = "mka", RegionCodeCodeId = _db.RegionCodeCodes.FirstOrDefault().Id, RegionCodeCodes = _db.RegionCodeCodes.FirstOrDefault()
            };

            if (isCreate)
            {
                _vehicle.RegNumbers = new List<RegNumbers>();
                _vehicle.RegNumbers.Add(_regNumber);
            }

            if (_vehicle == null && !isCreate)
            {
                throw new ArgumentException();
            }

            DataContext = _vehicle;

            FillComboBoxes();
            InitizlizeRegNumber();
            InitizlizeBackground();

            // VINTextBox.Text = _vehicle.VINCode.Trim();
            // manufacturerTextBox.Text = _vehicle.Manufacturer.Trim();
            // modelTextBox.Text = _vehicle.Model.Trim();
            // yearTextBox.Text = _vehicle.Year.ToString() + " г.";
            // weightTextBox.Text = _vehicle.Weight.ToString() + " кг.";
            // colorTextBlock.Text = _vehicle.CarColors.Name;
            // engineComboBox.SelectedItem = _vehicle.EngineTypes.LocalizedName;
            // driveComboBox.SelectedItem = _vehicle.TypeOfDrive.Name;
            // colorComboBox.SelectedItem = _vehicle.CarColors.Name;
            //
            // colorTextBlock.ToolTip = _vehicle.CarColors.ColorName;
        }

        public void FillComboBoxes()
        {
            var colors = _db.CarColors.ToList();

            colorComboBox.ItemsSource = colors;

            var engineTypes = _db.EngineTypes.ToList();

            engineComboBox.ItemsSource = engineTypes;

            var driveTypes = _db.TypeOfDrive.ToList();

            driveComboBox.ItemsSource = driveTypes;

            driverComboBox.ItemsSource = _db.Drivers.ToList();
        }
        
        public void InitizlizeBackground()
        {
            var bc = new BrushConverter();
            
            if (!_isCreate)
                colorTextBlock.Background = (System.Windows.Media.Brush)bc.ConvertFrom(_vehicle.CarColors.HexCode);
        }

        public void InitizlizeRegNumber()
        {
            var r = GetRegNumber();
            RenderRegNumber(r);
        }

        private void RenderRegNumber(RegNumber r)
        {
            firstSignLetter.Content = string.Join("", r.Series.ToString().Take(1)).ToUpper();
            secondSignLetter.Content = string.Join("", r.Series.ToString().Skip(1).Take(2)).ToUpper();
            signNumbers.Content = r.Number.ToString();
            signRegionCode.Content = r.RegionCode.ToString();

            signRegionCode.ToolTip = _regNumber.RegionCodeCodes.RegionCodes.RegionNameRU.ToString();
        }

        private RegNumber GetRegNumber()
        {
            var series = new RegNumberSeries(_regNumber.Series.ToLower());
            var number = new RegNumberNumber(_regNumber.Number);

            return new RegNumber(series, number, _regNumber.RegionCodeCodes.Code);
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _vehicle.DateCreated = DateTime.Now.Date;
                var state = !_isCreate ? EntityState.Modified : EntityState.Added;

                _db.Entry(_vehicle).State = state;

                if (state == EntityState.Added)
                    _db.Vehicles.Add(_vehicle);
            
                _db.SaveChanges();
            
                _parent.Refresh();

                MessageBox.Show("Успешно сохранено!", "Успех", MessageBoxButton.OK);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Проверьте введенные данные", "Внимание", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            
        }

        private void ColorComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedId = (int) colorComboBox.SelectedValue;

            var color = _db.CarColors.First(x => x.Id == selectedId);
            
            var bc = new BrushConverter();
            colorTextBlock.Background = (System.Windows.Media.Brush)bc.ConvertFrom(color.HexCode);
        }
    }
}
