using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.ViewModels.Recipes;

public class RecipeItemViewModel
{
    private readonly Recipe _recipe;

    public RecipeItemViewModel(Recipe recipe)
    {
        _recipe = recipe;
    }

    public Recipe Recipe => _recipe;

    public string Name => _recipe.Name;
    public string Ingredients => _recipe.Ingredients;
    public string Instructions => _recipe.Instructions ?? "";
    public string? Author => _recipe.Author;
    public string StarIcon => _recipe.IsHighlighted ? "⭐" : "☆";
}