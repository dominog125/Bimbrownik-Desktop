using System.Text.Json.Serialization;

namespace Bimbrownik_Desktop.Services.Data.Dtos;

public record CategoryDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string? Name
);