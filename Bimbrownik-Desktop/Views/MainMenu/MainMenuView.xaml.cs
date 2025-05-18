using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Recipes;

namespace Bimbrownik_Desktop.Views.MainMenu;

public partial class MainMenuView : UserControl
{
    public MainMenuView() => InitializeComponent();

    private void Recipes_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Instance.NavigateTo(new RecipesView());
    }
    private void Categories_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Kategorie");
    private void Notifications_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Powiadomienia");
    private void Statistics_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Statystyki");

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Instance.Logout(this);
    }
}