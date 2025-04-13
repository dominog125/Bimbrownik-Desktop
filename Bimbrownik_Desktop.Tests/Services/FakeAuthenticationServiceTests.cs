using Bimbrownik_Desktop.Services;
using Xunit;

namespace Bimbrownik_Desktop.Tests.Services;

public class FakeAuthenticationServiceTests
{
    [Fact]
    public void Login_WithCorrectCredentials_ReturnsTrue()
    {
        var service = new FakeAuthenticationService();

        var result = service.Login("admin", "1234");

        Assert.True(result);
    }

    [Theory]
    [InlineData("admin", "wrongpass")]
    [InlineData("wronguser", "1234")]
    [InlineData("user", "pass")]
    public void Login_WithInvalidCredentials_ReturnsFalse(string username, string password)
    {
        var service = new FakeAuthenticationService();

        var result = service.Login(username, password);

        Assert.False(result);
    }
}