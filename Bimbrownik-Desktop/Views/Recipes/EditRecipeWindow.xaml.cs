using System.Windows;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth.Session;

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
            Author = recipe.Author,
            AlcoholCategoryId = recipe.AlcoholCategoryId,
            AlcoholCategoryName = recipe.AlcoholCategoryName,
            IsHighlighted = recipe.IsHighlighted
        };

        TitleBox.Text = Recipe.Name;
        NameBox.Text = Recipe.Ingredients;
        InstructionsBox.Text = Recipe.Instructions;
        HighlightCheck.IsChecked = Recipe.IsHighlighted;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Recipe.Name = TitleBox.Text;
        Recipe.Ingredients = NameBox.Text;
        Recipe.Instructions = InstructionsBox.Text;
        Recipe.Author = NavigationService.Instance.Resolve<TokenStorage>().CurrentUser?.Username;
        Recipe.IsHighlighted = HighlightCheck.IsChecked == true;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}