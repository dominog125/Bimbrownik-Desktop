using System.Windows;
using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.Views.Recipes;

public partial class EditRecipeWindow : Window
{
    public Recipe Recipe { get; private set; }

    public EditRecipeWindow(Recipe recipe)
    {
        InitializeComponent();

        Recipe = new Recipe
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Ingredients = recipe.Ingredients,
            Instructions = recipe.Instructions,
            CreatedAt = recipe.CreatedAt,
            IsHighlighted = recipe.IsHighlighted
        };

        NameBox.Text = Recipe.Name;
        IngredientsBox.Text = Recipe.Ingredients;
        InstructionsBox.Text = Recipe.Instructions;
        HighlightCheck.IsChecked = Recipe.IsHighlighted;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Recipe.Name = NameBox.Text;
        Recipe.Ingredients = IngredientsBox.Text;
        Recipe.Instructions = InstructionsBox.Text;
        Recipe.IsHighlighted = HighlightCheck.IsChecked == true;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}