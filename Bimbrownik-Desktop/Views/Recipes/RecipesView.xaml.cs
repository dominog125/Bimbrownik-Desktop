using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using System;

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
                var block = new TextBlock
                {
                    Text = $"{recipe.Name}\n{recipe.Ingredients}\n{recipe.Instructions}\n{recipe.CreatedAt:yyyy-MM-dd HH:mm}",
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 10),
                    FontWeight = recipe.IsHighlighted ? FontWeights.Bold : FontWeights.Normal
                };

                var editButton = new Button
                {
                    Content = "✏️",
                    Width = 32,
                    Height = 32,
                    Margin = new Thickness(4)
                };
                editButton.Click += (s, e) => EditRecipe(recipe);

                var deleteButton = new Button
                {
                    Content = "🗑️",
                    Width = 32,
                    Height = 32,
                    Margin = new Thickness(4)
                };
                deleteButton.Click += (s, e) => DeleteRecipe(recipe);

                var starButton = new Button
                {
                    Content = recipe.IsHighlighted ? "⭐" : "☆",
                    Width = 32,
                    Height = 32,
                    Margin = new Thickness(4)
                };
                starButton.Click += (s, e) => ToggleHighlight(recipe);

                var buttons = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(10, 0, 0, 10)
                };
                buttons.Children.Add(editButton);
                buttons.Children.Add(deleteButton);
                buttons.Children.Add(starButton);

                var wrapper = new StackPanel();
                wrapper.Children.Add(block);
                wrapper.Children.Add(buttons);

                RecipeList.Children.Add(wrapper);
            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid().GetHashCode(),
                Name = "Nowy przepis",
                Ingredients = "",
                Instructions = "",
                CreatedAt = DateTime.Now,
                IsHighlighted = false
            };

            var all = _service.LoadRecipes();
            all.Add(newRecipe);
            _service.SaveAllRecipes(all);
            LoadRecipes();
        }

        private void DeleteRecipe(Recipe recipe)
        {
            var all = _service.LoadRecipes();
            all.RemoveAll(r => r.Id == recipe.Id);
            _service.SaveAllRecipes(all);
            LoadRecipes();
        }

        private void EditRecipe(Recipe recipe)
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
        private void ToggleHighlight(Recipe recipe)
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

    