using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.ViewModels.Recipes;

namespace Bimbrownik_Desktop.Views.Recipes
{
    public partial class RecipeItemView : UserControl
    {
        public event EventHandler<Recipe>? EditRequested;
        public event EventHandler<Recipe>? DeleteRequested;
        public event EventHandler<Recipe>? HighlightToggled;

        private Recipe? Recipe => (DataContext as RecipeItemViewModel)?.Recipe;

        public RecipeItemView()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Recipe != null)
                EditRequested?.Invoke(this, Recipe);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Recipe != null)
                DeleteRequested?.Invoke(this, Recipe);
        }

        private void ToggleHighlight_Click(object sender, RoutedEventArgs e)
        {
            if (Recipe != null)
                HighlightToggled?.Invoke(this, Recipe);
        }
    }
}