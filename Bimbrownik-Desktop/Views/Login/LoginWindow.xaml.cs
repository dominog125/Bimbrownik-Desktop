using System.Windows;
using Bimbrownik_Desktop.ViewModels;
using Bimbrownik_Desktop.Views.Main;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using System.Windows.Controls;

namespace Bimbrownik_Desktop.Views.Login;

public partial class LoginWindow : Window
{
    private readonly LoginViewModel viewModel;

    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        DataContext = viewModel;

        PasswordBox.PasswordChanged += (_, _) => viewModel.Password = PasswordBox.Password;

        viewModel.LoginSucceeded += OnLoginSucceeded;
        viewModel.LoginFailed += ShowLoginFailedMessage;
    }
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
            vm.Password = ((PasswordBox)sender).Password;
    }

    private void OnLoginSucceeded(LoginResult result)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();

        Application.Current.MainWindow = mainWindow;
        this.Close();
    }

    private void ShowLoginFailedMessage(string message)
    {
        MessageBox.Show(message, "Logowanie nie powiodło się", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}