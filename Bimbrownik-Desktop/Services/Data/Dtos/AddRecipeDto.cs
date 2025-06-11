namespace Bimbrownik_Desktop.Services.Data.Dtos;

public record AddRecipeDto(
    Guid Id,
    string? Name,
    string? Description,
    string? Title,
    Guid AlcoholCategoryId
);