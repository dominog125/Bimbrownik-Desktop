using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.ViewModels.Recipes;

namespace Bimbrownik_Desktop.Views.Recipes
{
    public partial class RecipesView : UserControl
    {
        private readonly RecipeService _service = new();

        public RecipesView()
        {
            InitializeComponent();
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            RecipeList.Children.Clear();

            var recipes = _service.LoadRecipes();
            foreach (var recipe in recipes)
            {
                var item = new RecipeItemView
                {
                    DataContext = new RecipeItemViewModel(recipe)
                };

                item.EditRequested += EditRecipe;
                item.DeleteRequested += DeleteRecipe;
                item.HighlightToggled += ToggleHighlight;

                RecipeList.Children.Add(item);
            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid().GetHashCode(),
                Name = "",
                Ingredients = "",
                Instructions = "",
                CreatedAt = DateTime.Now,
                IsHighlighted = false
            };

            var dialog = new EditRecipeWindow(newRecipe);

            if (dialog.ShowDialog() == true)
            {
                var all = _service.LoadRecipes();
                all.Add(dialog.Recipe);
                _service.SaveAllRecipes(all);
                LoadRecipes();
            }
        }

        private void DeleteRecipe(object? sender, Recipe recipe)
        {
            var all = _service.LoadRecipes();
            all.RemoveAll(r => r.Id == recipe.Id);
            _service.SaveAllRecipes(all);
            LoadRecipes();
        }

        private void EditRecipe(object? sender, Recipe recipe)
        {
            var dialog = new EditRecipeWindow(recipe);
            if (dialog.ShowDialog() == true)
            {
                var updated = dialog.Recipe;
                var all = _service.LoadRecipes();
                var index = all.FindIndex(r => r.Id == updated.Id);

                if (index >= 0)
                {
                    all[index] = updated;
                    _service.SaveAllRecipes(all);
                    LoadRecipes();
                }
            }
        }

        private void ToggleHighlight(object? sender, Recipe recipe)
        {
            var all = _service.LoadRecipes();
            var found = all.Find(r => r.Id == recipe.Id);
            if (found != null)
            {
                found.IsHighlighted = !found.IsHighlighted;
                _service.SaveAllRecipes(all);
                LoadRecipes();
            }
        }
    }
}
