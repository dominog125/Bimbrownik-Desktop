using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Services.Data.Mappers;

public static class RecipeMapper
{
    public static Recipe FromPostResponseDto(RecipeDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Title ?? "",
        Ingredients = dto.Name ?? "",
        Instructions = dto.Description ?? "",
        Author = dto.Author ?? "",
        AlcoholCategoryId = dto.AlcoholCategoryId,
        AlcoholCategoryName = dto.AlcoholCategoryName,
        IsHighlighted = false 
    };

    public static AddRecipeDto ToAddRecipeDto(Recipe recipe) => new(
        recipe.Id,
        recipe.Ingredients,
        recipe.Instructions,
        recipe.Name,
        recipe.AlcoholCategoryId
    );

    public static UpdateRecipeDto ToUpdateRecipeDto(Recipe recipe) => new(
        recipe.Id,
        recipe.Ingredients,
        recipe.Instructions,
        recipe.Author,
        recipe.Name,
        recipe.AlcoholCategoryId,
        new CategoryDto(recipe.AlcoholCategoryId, recipe.AlcoholCategoryName)
    );
}