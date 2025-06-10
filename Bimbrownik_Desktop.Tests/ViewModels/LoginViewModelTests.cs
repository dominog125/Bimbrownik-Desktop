using System;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.ViewModels;
using Moq;
using Xunit;

namespace Bimbrownik_Desktop.Tests.ViewModels;

public class LoginViewModelTests
{
    [Fact]
    public async Task LoginAsync_WithValidCredentials_RaisesLoginSucceededAndSavesToken()
    {
        var authMock = new Mock<AuthenticationApiClient>();
        var tokenMock = new Mock<TokenStorage>();

        var expectedResult = new LoginResult("mockToken", "admin", "Admin");
        authMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
                .ReturnsAsync(expectedResult);

        var viewModel = new LoginViewModel(authMock.Object, tokenMock.Object)
        {
            Email = "admin@example.com",
            Password = "1234"
        };

        LoginResult? result = null;
        viewModel.LoginSucceeded += r => result = r;

        await viewModel.LoginAsync();

        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
        tokenMock.Verify(x => x.SaveToken("mockToken"), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WithException_RaisesLoginFailed()
    {
        var authMock = new Mock<AuthenticationApiClient>();
        var tokenMock = new Mock<TokenStorage>();

        authMock.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
                .ThrowsAsync(new Exception("Invalid credentials"));

        var viewModel = new LoginViewModel(authMock.Object, tokenMock.Object)
        {
            Email = "admin@example.com",
            Password = "wrong"
        };

        string? errorMessage = null;
        viewModel.LoginFailed += msg => errorMessage = msg;

        await viewModel.LoginAsync();

        Assert.NotNull(errorMessage);
        Assert.Contains("Invalid credentials", errorMessage);
        tokenMock.Verify(x => x.SaveToken(It.IsAny<string>()), Times.Never);
    }

    [Theory]
    [InlineData("", "1234", false)]
    [InlineData("admin@example.com", "", false)]
    [InlineData("admin@example.com", "1234", true)]
    public void CanLogin_ReturnsExpected(string email, string password, bool expected)
    {
        var authMock = new Mock<AuthenticationApiClient>();
        var tokenMock = new Mock<TokenStorage>();

        var viewModel = new LoginViewModel(authMock.Object, tokenMock.Object)
        {
            Email = email,
            Password = password
        };

        var canLogin = viewModel.LoginCommand.CanExecute(null);

        Assert.Equal(expected, canLogin);
    }
    [Fact]
    public async Task LoginAsync_WhenAuthFails_InvokesLoginFailed()
    {
        var mockAuth = new Mock<AuthenticationApiClient>();
        mockAuth.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
                .ThrowsAsync(new HttpRequestException("401 Unauthorized"));

        var mockToken = new Mock<TokenStorage>();
        var viewModel = new LoginViewModel(mockAuth.Object, mockToken.Object);

        string? errorMessage = null;
        viewModel.LoginFailed += msg => errorMessage = msg;

        viewModel.Email = "admin";
        viewModel.Password = "wrongpass";

        await viewModel.LoginAsync();

        Assert.Contains("401", errorMessage);
    }
}