using Bimbrownik_Desktop.Services.Auth.Dtos;

namespace Bimbrownik_Desktop.Services.Auth.Session;

public class TokenStorage
{
    private string? _token;
    private LoginResult? _currentUser;

    public string? Token => _token;
    public LoginResult? CurrentUser => _currentUser;
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(_token);

    public void Save(string token, LoginResult user)
    {
        _token = token;
        _currentUser = user;
    }

    public void ClearToken()
    {
        _token = null;
        _currentUser = null;
    }
}