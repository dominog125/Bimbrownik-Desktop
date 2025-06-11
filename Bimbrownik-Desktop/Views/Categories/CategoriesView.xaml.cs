using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Views.Categories;

public partial class CategoriesView : UserControl
{
    private readonly CategoryService _service;

    public CategoriesView()
    {
        InitializeComponent();

        _service = NavigationService.Instance.Resolve<CategoryService>();

        Loaded += async (_, _) => await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        CategoryList.Children.Clear();

        List<CategoryDto> categories = await _service.LoadCategoriesAsync();

        foreach (var category in categories)
        {
            var block = new TextBlock
            {
                Text = category.Name,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 10)
            };
            CategoryList.Children.Add(block);
        }
    }

    private async void AddCategory_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new AddCategoryWindow();
        if (dialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(dialog.CategoryName))
        {
            await _service.AddCategoryAsync(dialog.CategoryName.Trim());
            await LoadCategoriesAsync();
        }
    }
}