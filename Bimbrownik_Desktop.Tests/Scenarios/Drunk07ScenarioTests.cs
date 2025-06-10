using System;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.ViewModels;
using Bimbrownik_Desktop.Views.Login;
using Moq;
using Xunit;

namespace Bimbrownik_Desktop.Tests.Scenarios;

public class Drunk07ScenarioTests
{
    [Fact]
    public async Task FullLoginScenario_WithValidCredentials_LogsInSuccessfully()
    {
        var mockApi = new Mock<AuthenticationApiClient>();
        var tokenStorage = new TokenStorage();

        var expectedToken = "jwt.token.value";
        var loginResult = new LoginResult(expectedToken, "admin", "Administrator");

        mockApi.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
               .ReturnsAsync(loginResult);

        var viewModel = new LoginViewModel(mockApi.Object, tokenStorage)
        {
            Email = "admin@example.com",
            Password = "secure123"
        };

        LoginResult? result = null;
        viewModel.LoginSucceeded += r => result = r;

        var window = new LoginWindow(viewModel);

        await viewModel.LoginAsync();

        Assert.NotNull(result);
        Assert.Equal(expectedToken, tokenStorage.GetToken());
        Assert.Equal("admin", result!.Username);
        Assert.Equal("Administrator", result.Role);
    }

    [Fact]
    public async Task FullLoginScenario_WithInvalidCredentials_ShowsLoginFailed()
    {
        var mockApi = new Mock<AuthenticationApiClient>();
        var tokenStorage = new TokenStorage();

        mockApi.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
               .ThrowsAsync(new Exception("401 Unauthorized"));

        var viewModel = new LoginViewModel(mockApi.Object, tokenStorage)
        {
            Email = "admin@example.com",
            Password = "wrong"
        };

        string? errorMessage = null;
        viewModel.LoginFailed += msg => errorMessage = msg;

        var window = new LoginWindow(viewModel);

        await viewModel.LoginAsync();

        Assert.Null(tokenStorage.GetToken());
        Assert.NotNull(errorMessage);
        Assert.Contains("401", errorMessage);
    }
}
