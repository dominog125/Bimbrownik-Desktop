using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Bimbrownik_Desktop.Utilities;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.Services.Offline;

namespace Bimbrownik_Desktop.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthenticationApiClient _auth;
    private readonly TokenStorage tokenStorage;
    private readonly OfflineStorage? offlineStorage;

    public LoginViewModel(AuthenticationApiClient auth, TokenStorage tokenStorage, OfflineStorage offlineStorage)
    {
        _auth = auth;
        this.tokenStorage = tokenStorage;
        this.offlineStorage = offlineStorage;
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

    private async Task LoginAsync()
    {
        try
        {
            var result = await _auth.LoginAsync(new LoginRequestDto(Email, Password));

            if (offlineStorage is not null)
            {
                var recipes = await _auth.GetRecipesAsync();
                var categories = await _auth.GetCategoriesAsync();
                var statistics = await _auth.GetStatisticsAsync();

                await offlineStorage.SaveAllAsync(recipes, categories, statistics);
            }

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