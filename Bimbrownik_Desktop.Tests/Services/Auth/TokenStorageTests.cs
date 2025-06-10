using Bimbrownik_Desktop.Services.Auth.Session;
using Xunit;

namespace Bimbrownik_Desktop.Tests.Services.Auth;

public class TokenStorageTests
{
    [Fact]
    public void SaveToken_StoresTokenCorrectly()
    {
        var storage = new TokenStorage();

        storage.SaveToken("abc123");

        Assert.Equal("abc123", storage.GetToken());
    }

    [Fact]
    public void ClearToken_RemovesStoredToken()
    {
        var storage = new TokenStorage();
        storage.SaveToken("token");

        storage.ClearToken();

        Assert.Null(storage.GetToken());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("valid_token", true)]
    public void IsAuthenticated_ReturnsExpected(string? token, bool expected)
    {
        var storage = new TokenStorage();
        if (token != null) storage.SaveToken(token);

        Assert.Equal(expected, storage.IsAuthenticated);
    }
}