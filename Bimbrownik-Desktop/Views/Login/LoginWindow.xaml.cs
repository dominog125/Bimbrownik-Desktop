using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Main;

namespace Bimbrownik_Desktop.Views.Login
{
    public partial class LoginWindow : Window
    {
        private readonly FakeAuthenticationService _auth = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_auth.Login(UsernameBox.Text, PasswordBox.Password))
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.MainWindow = mainWindow;
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędne dane logowania!");
            }
        }
    }
}