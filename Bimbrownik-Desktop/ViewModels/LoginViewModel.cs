using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bimbrownik_Desktop.Utilities;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;

namespace Bimbrownik_Desktop.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthenticationApiClient _auth;
    private readonly TokenStorage tokenStorage;

    public LoginViewModel(AuthenticationApiClient auth, TokenStorage tokenStorage)
    {
        _auth = auth;
        this.tokenStorage = tokenStorage;
        loginCommand = new AsyncRelayCommand(LoginAsync, CanLogin);
    }

    private string email = "";
    public string Email
    {
        get => email;
        set { email = value; OnPropertyChanged(nameof(Email)); loginCommand.RaiseCanExecuteChanged(); }
    }

    private string password = "";
    public string Password
    {
        get => password;
        set { password = value; OnPropertyChanged(nameof(Password)); loginCommand.RaiseCanExecuteChanged(); }
    }

    private readonly AsyncRelayCommand loginCommand;
    public ICommand LoginCommand => loginCommand;

    public event Action<LoginResult>? LoginSucceeded;
    public event Action<string>? LoginFailed;

    private bool CanLogin() =>
        !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

    internal async Task LoginAsync()
    {
        try
        {
            var result = await _auth.LoginAsync(new LoginRequestDto(Email, Password));

            tokenStorage.SaveToken(result.Token);

            LoginSucceeded?.Invoke(result);
        }
        catch (Exception ex)
        {
            LoginFailed?.Invoke(ex.Message);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}