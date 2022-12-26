using System.Windows;
using Practics.GIBDD.App.Exceptions;
using Practics.GIBDD.App.Services;

namespace Practics.GIBDD.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window
    {
        private readonly AuthorizationService _authorizationService;
        private readonly bool _DEBUG = true;

        public AuthorizationView()
        {
            InitializeComponent();
            _authorizationService = new AuthorizationService();

            if (_DEBUG)
            {
                loginTextBox.Text = "inspector";
                passwordBox.Password = "inspector";
            }
        }

        private void authorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;

            try
            {
                var user = _authorizationService.Auth(login, password);
                
                var view = new ChooseNextView();
                view.Show();
                
                Close();
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Пользователь не найден");
            }
            catch (TooManyAuthorizationAttempts)
            {
                MessageBox.Show("Превышено число попыток. Попробуйте позже");
            }
            catch (AuthenticationFail)
            {
                MessageBox.Show("Неверный пароль");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _authorizationService.Dispose();
        }
    }
}
