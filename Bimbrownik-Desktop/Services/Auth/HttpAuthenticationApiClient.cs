using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth.Dtos;

namespace Bimbrownik_Desktop.Services.Auth;

public class HttpAuthenticationApiClient : AuthenticationApiClient
{
    private readonly HttpClient _httpClient;

    public HttpAuthenticationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResult> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PostAsync("api/Auth/Login", content, ct);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<LoginResult>(responseContent)!;
    }
}