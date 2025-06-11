using System;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Services.Data.Dtos;

public record UpdateRecipeDto(
    Guid Id,
    string? Name,
    string? Description,
    string? Author,
    string? Title,
    Guid AlcoholCategoryId,
    CategoryDto AlcoholCategory
);