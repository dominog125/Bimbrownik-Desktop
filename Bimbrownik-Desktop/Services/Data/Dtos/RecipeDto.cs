using System.Text.Json.Serialization;

namespace Bimbrownik_Desktop.Services.Data.Dtos;

public record RecipeDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("author")] string? Author,
    [property: JsonPropertyName("title")] string? Title,
    [property: JsonPropertyName("alcoholCategoryId")] Guid AlcoholCategoryId,
    [property: JsonPropertyName("alcoholCategoryName")] string? AlcoholCategoryName
);