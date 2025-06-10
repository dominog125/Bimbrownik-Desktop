using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.ViewModels;
using Moq;

public class Drunk01ScenarioTests
{
    [Fact]
    public async Task FullLoginScenario_StoresToken_WhenCredentialsValid()
    {
        var expectedToken = "abc.def.ghi";

        var mockApi = new Mock<AuthenticationApiClient>();
        mockApi.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDto>(), default))
               .ReturnsAsync(new LoginResult(expectedToken, "admin", "Admin"));

        var tokenStorage = new TokenStorage();

        var viewModel = new LoginViewModel(mockApi.Object, tokenStorage);

        viewModel.Email = "admin";
        viewModel.Password = "1234";

        await viewModel.LoginAsync();

        Assert.Equal(expectedToken, tokenStorage.GetToken());
    }
}