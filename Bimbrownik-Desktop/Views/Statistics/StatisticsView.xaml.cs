using System;
using System.Linq;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;

namespace Bimbrownik_Desktop.Views.Statistics;

public partial class StatisticsView : UserControl
{
    private readonly RecipeService _recipeService = new();
    private readonly CategoryService _categoryService = new();

    public StatisticsView()
    {
        InitializeComponent();
        LoadStatistics();
    }

    private void LoadStatistics()
    {
        var recipes = _recipeService.LoadRecipes();
        var categories = _categoryService.GetAllCategories();

        TotalRecipesText.Text = recipes.Count.ToString();
        TotalCategoriesText.Text = categories.Count.ToString();

        var today = DateTime.Today;
        var viewedToday = recipes.Count(r => r.LastViewedAt.HasValue && r.LastViewedAt.Value.Date == today);
        ViewedTodayText.Text = viewedToday.ToString();

        var lastViewed = recipes
            .Where(r => r.LastViewedAt.HasValue)
            .OrderByDescending(r => r.LastViewedAt)
            .FirstOrDefault();

        LastViewedText.Text = lastViewed != null
            ? $"{lastViewed.Name} ({lastViewed.LastViewedAt:yyyy-MM-dd HH:mm})"
            : "-";

        var topViewed = recipes
            .OrderByDescending(r => r.ViewCount)
            .Take(3)
            .ToList();

        TopViewedList.Children.Clear();
        foreach (var recipe in topViewed)
        {
            TopViewedList.Children.Add(new TextBlock
            {
                Text = $"{recipe.Name} ({recipe.ViewCount} wyświetleń)",
                Margin = new System.Windows.Thickness(0, 0, 0, 5)
            });
        }
    }
}