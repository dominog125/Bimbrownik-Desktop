using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.ViewModels.Recipes
{
    public class RecipeItemViewModel
    {
        public Recipe Recipe { get; }

        public string Name => Recipe.Name;
        public string Ingredients => Recipe.Ingredients;
        public string Instructions => Recipe.Instructions;
        public DateTime CreatedAt => Recipe.CreatedAt;
        public bool IsHighlighted => Recipe.IsHighlighted;
        public string StarIcon => Recipe.IsHighlighted ? "⭐" : "☆";

        public RecipeItemViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }
    }
}
