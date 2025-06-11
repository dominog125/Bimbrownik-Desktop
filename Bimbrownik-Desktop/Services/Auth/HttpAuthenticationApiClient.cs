using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Services.Auth;

public class HttpAuthenticationApiClient : AuthenticationApiClient
{
    private readonly HttpClient _httpClient;
    private readonly TokenStorage _tokenStorage;

    public HttpAuthenticationApiClient(HttpClient httpClient, TokenStorage tokenStorage)
    {
        _httpClient = httpClient;
        _tokenStorage = tokenStorage;
    }

    public async Task<LoginResult> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await _httpClient.PostAsync("api/Auth/Login", content, ct);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(ct);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var result = JsonSerializer.Deserialize<LoginResult>(body, options)!;

        _tokenStorage.Save(result.Token, result);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", result.Token);

        return result;
    }

    public async Task<List<RecipeDto>> GetRecipesAsync(CancellationToken ct = default)
    {
        EnsureToken();

        var request = new HttpRequestMessage(HttpMethod.Get, "api/Posts");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<List<RecipeDto>>(content)!;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct = default)
    {
        EnsureToken();

        var request = new HttpRequestMessage(HttpMethod.Get, "api/AlcoholCategories");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<List<CategoryDto>>(content)!;
    }

    public async Task<StatisticsDto?> GetStatisticsAsync(CancellationToken ct = default)
    {
        EnsureToken();

        var request = new HttpRequestMessage(HttpMethod.Get, "api/Statistics");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<StatisticsDto>(json);
    }

    public async Task AddRecipeAsync(AddRecipeDto dto, CancellationToken ct = default)
    {
        EnsureToken();

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "api/Posts")
        {
            Content = content
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateRecipeAsync(UpdateRecipeDto dto, CancellationToken ct = default)
    {
        EnsureToken();

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, $"api/Posts/{dto.Id}")
        {
            Content = content
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRecipeAsync(Guid id, CancellationToken ct = default)
    {
        EnsureToken();

        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Posts/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStorage.Token);

        using var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddCategoryAsync(string name)
    {
        var category = new CategoryDto(Guid.Empty, name);
        var response = await _httpClient.PostAsJsonAsync("/api/AlcoholCategories", category);
        response.EnsureSuccessStatusCode();
    }

    private void EnsureToken()
    {
        if (string.IsNullOrWhiteSpace(_tokenStorage.Token))
            throw new InvalidOperationException("Token is missing");
    }
}