using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views;          // ← dodaj, żeby widzieć MainWindow

namespace Bimbrownik_Desktop.Views.Login
{
    public partial class LoginWindow : Window
    {
        private readonly FakeAuthenticationService _auth = new();

        public LoginWindow() => InitializeComponent();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_auth.Login(UsernameBox.Text, PasswordBox.Password))
            {
                // otwieramy główne okno z ContentArea
                new MainWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Błędne dane logowania!");
            }
        }
    }
}