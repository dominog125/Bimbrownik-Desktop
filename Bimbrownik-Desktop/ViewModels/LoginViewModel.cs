using System.ComponentModel;
using Bimbrownik_Desktop.Services;

namespace Bimbrownik_Desktop.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly AuthenticationService _auth;

    public LoginViewModel(AuthenticationService auth) => _auth = auth;

    private string _username = "";
    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(nameof(Username)); }
    }

    private string _password = "";
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    public bool Authenticate() => _auth.Login(Username, Password);

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string prop) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}