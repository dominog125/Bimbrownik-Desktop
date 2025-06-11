using System.Text.Json.Serialization;

namespace Bimbrownik_Desktop.Services.Auth.Dtos;

public record LoginResult(
    [property: JsonPropertyName("jwtToken")] string Token,
    [property: JsonPropertyName("username")] string Username,
    string? Role = null);