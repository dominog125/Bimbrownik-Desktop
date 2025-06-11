using System.Windows.Controls;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Views.Statistics;

public partial class StatisticsView : UserControl
{
    private readonly AuthenticationApiClient _apiClient;

    public StatisticsView()
    {
        InitializeComponent();
        _apiClient = NavigationService.Instance.Resolve<AuthenticationApiClient>();
        Loaded += async (_, _) => await LoadStatisticsAsync();
    }

    private async Task LoadStatisticsAsync()
    {
        StatisticsDto stats;

        try
        {
            stats = await _apiClient.GetStatisticsAsync();
        }
        catch
        {
            TotalRecipesText.Text = "Błąd";
            TotalUsersText.Text = "Błąd";
            TotalCommentsText.Text = "Błąd";
            TotalCategoriesText.Text = "Błąd";
            return;
        }

        TotalRecipesText.Text = stats.TotalPosts.ToString();
        TotalUsersText.Text = stats.TotalUsers.ToString();
        TotalCommentsText.Text = stats.TotalComments.ToString();
        TotalCategoriesText.Text = stats.TotalCategories.ToString();
    }
}