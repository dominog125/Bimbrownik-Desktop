using System.Text.Json.Serialization;

namespace Bimbrownik_Desktop.Services.Data.Dtos;

public record StatisticsDto(
    [property: JsonPropertyName("totalPosts")] int TotalPosts,
    [property: JsonPropertyName("totalUsers")] int TotalUsers,
    [property: JsonPropertyName("totalComments")] int TotalComments,
    [property: JsonPropertyName("totalCategories")] int TotalCategories
);