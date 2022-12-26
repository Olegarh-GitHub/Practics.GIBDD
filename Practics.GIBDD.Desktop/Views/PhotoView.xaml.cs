using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Practics.GIBDD.App;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для PhotoView.xaml
    /// </summary>
    public partial class PhotoView : Window
    {
        private readonly ApplicationContext _db;
        private readonly int _photoId;
        public PhotoView(int id)
        {
            _db = new ApplicationContext();
            InitializeComponent();
            _photoId = id;

            var photo = _db.Photos.FirstOrDefault(x => x.Id == _photoId);
            if (photo == null)
            {
                throw new ArgumentException();
            }

            var image = new BitmapImage();

            using (var mem = new MemoryStream(photo.Data))
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

            displayImage.Source = image;
        }
    }
}
