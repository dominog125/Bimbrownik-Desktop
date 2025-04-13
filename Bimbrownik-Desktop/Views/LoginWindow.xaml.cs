using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.ViewModels;

namespace Bimbrownik_Desktop.Views;

public partial class LoginWindow : Window
{
    private readonly LoginViewModel viewModel;

    public LoginWindow()
    {
        InitializeComponent();
        viewModel = new LoginViewModel(new FakeAuthenticationService());
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        if (viewModel.Authenticate(UsernameTextBox.Text, PasswordBox.Password))
        {
            OpenMainWindow();
            return;
        }

        MessageBox.Show("Błędne dane logowania!");
    }

    private void OpenMainWindow()
    {
        new MainWindow().Show();
        Close();
    }
}