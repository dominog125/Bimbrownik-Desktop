using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.ViewModels.Recipes;

namespace Bimbrownik_Desktop.Views.Recipes
{
    public partial class RecipesView : UserControl
    {
        private readonly RecipeService _service;

        public RecipesView(RecipeService service)
        {
            InitializeComponent();
            _service = service;
            LoadRecipes();
        }

        private async Task LoadRecipes()
        {
            RecipeList.Children.Clear();

            var recipes = await _service.LoadRecipesAsync();
            foreach (var r in recipes)
            {
                Console.WriteLine($"[VM DEBUG] {r.Name} - {r.Instructions} - {r.Author}");
            }
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

        private async void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Name = "",
                Instructions = "",
                Author = NavigationService.Instance.Resolve<TokenStorage>().CurrentUser?.Username,
                IsHighlighted = false
            };

            var dialog = new EditRecipeWindow(newRecipe);

            if (dialog.ShowDialog() == true)
            {
                await _service.AddRecipeAsync(dialog.Recipe);
                await LoadRecipes();
            }
        }

        private async void DeleteRecipe(object? sender, Recipe recipe)
        {
            await _service.DeleteRecipeAsync(recipe.Id);
            await LoadRecipes();
        }

        private async void EditRecipe(object? sender, Recipe recipe)
        {
            var dialog = new EditRecipeWindow(recipe);
            if (dialog.ShowDialog() == true)
            {
                await _service.UpdateRecipeAsync(dialog.Recipe);
                await LoadRecipes();
            }
        }

        private async void ToggleHighlight(object? sender, Recipe recipe)
        {
            recipe.IsHighlighted = !recipe.IsHighlighted;
            await _service.UpdateRecipeAsync(recipe);
            await LoadRecipes();
        }
    }
}