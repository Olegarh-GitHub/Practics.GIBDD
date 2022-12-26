using System.Text;
using System.Windows;
using Microsoft.Win32;
using Practics.GIBDD.App.Services;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ExportFSSPView.xaml
    /// </summary>
    public partial class ExportFSSPView : Window
    {
        private readonly FSSPExporterService _fsspExporterService;
        public ExportFSSPView()
        {
            InitializeComponent();
            _fsspExporterService = new FSSPExporterService();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            var fines = _fsspExporterService.GetFinesToExport();

            var csv = new StringBuilder();

            foreach (var f in fines)
            {
                csv.AppendLine($"{f.Id}; {f.ExternalId}; {f.CarNum}; {f.LicenseNum}");
            }

            var sfd = new SaveFileDialog()
            {
                Filter = "csv files (*.csv)|*.csv"
            };
            if (sfd.ShowDialog() == true)
            {
                var filename = sfd.FileName;
                System.IO.File.WriteAllText(filename, csv.ToString());
                
                MessageBox.Show("Отлично. Мы их накроем, босс.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            _fsspExporterService.SendToFSSP(fines);
        }
    }
}
