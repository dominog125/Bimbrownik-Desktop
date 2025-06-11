using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Categories;
using Bimbrownik_Desktop.Views.Notifications;
using Bimbrownik_Desktop.Views.Recipes;
using Bimbrownik_Desktop.Views.Statistics;

namespace Bimbrownik_Desktop.Views.MainMenu
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private readonly Dictionary<string, UserControl> views = new()
        {
            { "Recipes", NavigationService.Instance.Resolve<RecipesView>() },
            { "Categories", NavigationService.Instance.Resolve<CategoriesView>() },
            { "Notifications", new NotificationsView() },
            { "Statistics", new StatisticsView() }
        };

        private void Navigate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string key && views.TryGetValue(key, out var view))
                NavigationService.Instance.NavigateTo(view);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Instance.Logout(this);
        }

    }
}