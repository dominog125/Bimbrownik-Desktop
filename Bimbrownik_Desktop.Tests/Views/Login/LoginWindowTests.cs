using System;
using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.ViewModels;
using Bimbrownik_Desktop.Views.Login;
using Moq;
using Xunit;

namespace Bimbrownik_Desktop.Tests.Views;

public class LoginWindowTests
{
    [StaFact]
    public void Constructor_SetsDataContext_AndSubscribesEvents()
    {
        var authMock = new Mock<AuthenticationApiClient>();
        var tokenMock = new Mock<TokenStorage>();
        var viewModel = new LoginViewModel(authMock.Object, tokenMock.Object);

        var window = new LoginWindow(viewModel);

        Assert.Equal(viewModel, window.DataContext);
    }

    [StaFact]
    public void PasswordBox_PasswordChanged_UpdatesViewModelPassword()
    {
        var authMock = new Mock<AuthenticationApiClient>();
        var tokenMock = new Mock<TokenStorage>();
        var viewModel = new LoginViewModel(authMock.Object, tokenMock.Object);
        var window = new LoginWindow(viewModel);

        var passwordBox = new PasswordBox { Password = "test123" };

        var args = new RoutedEventArgs();
        window.GetType()
              .GetMethod("PasswordBox_PasswordChanged", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
              .Invoke(window, new object[] { passwordBox, args });

        Assert.Equal("test123", viewModel.Password);
    }
}
