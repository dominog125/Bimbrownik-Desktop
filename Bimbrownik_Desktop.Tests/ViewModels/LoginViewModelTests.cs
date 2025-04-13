using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.ViewModels;
using Xunit;

namespace Bimbrownik_Desktop.Tests.ViewModels;

public class LoginViewModelTests
{
    [Fact]
    public void Authenticate_WithValidCredentials_ReturnsTrue()
    {
        var viewModel = new LoginViewModel(new FakeAuthenticationService());

        var result = viewModel.Authenticate("admin", "1234");

        Assert.True(result);
    }

    [Fact]
    public void Authenticate_WithInvalidCredentials_ReturnsFalse()
    {
        var viewModel = new LoginViewModel(new FakeAuthenticationService());

        var result = viewModel.Authenticate("wrong", "user");

        Assert.False(result);
    }
}