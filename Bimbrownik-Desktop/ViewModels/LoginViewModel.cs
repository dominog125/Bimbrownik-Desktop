using Bimbrownik_Desktop.Services;

namespace Bimbrownik_Desktop.ViewModels;

public class LoginViewModel
{
    private readonly AuthenticationService authenticationService;

    public LoginViewModel(AuthenticationService service)
    {
        authenticationService = service;
    }

    public bool Authenticate(string username, string password) =>
        authenticationService.Login(username, password);
}