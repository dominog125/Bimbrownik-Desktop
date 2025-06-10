using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Xunit;

namespace Bimbrownik_Desktop.Tests.Services.Auth;

public class HttpAuthenticationApiClientTests
{
    [Fact]
    public async Task LoginAsync_WithValidResponse_ReturnsLoginResult()
    {
        var expectedJson = """
        {
            "token": "abc123",
            "username": "admin",
            "role": "Administrator"
        }
        """;

        var handler = new FakeHttpMessageHandler((request, ct) =>
        {
            Assert.Equal("api/Auth/Login", request.RequestUri!.ToString());
            Assert.Equal(HttpMethod.Post, request.Method);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedJson)
            });
        });

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new System.Uri("https://fakeapi.com/")
        };

        var client = new HttpAuthenticationApiClient(httpClient);
        var dto = new LoginRequestDto("admin@example.com", "secret");

        var result = await client.LoginAsync(dto);

        Assert.Equal("abc123", result.Token);
        Assert.Equal("admin", result.Username);
        Assert.Equal("Administrator", result.Role);
    }

    private class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public FakeHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}