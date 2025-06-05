using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Categories;
using System.Collections.Generic;

namespace Bimbrownik_Desktop.Views.Categories
{
    public partial class CategoriesView : UserControl
    {
        private readonly CategoryService _service = new();

        public CategoriesView()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            CategoryList.Children.Clear();

            List<DrinkCategory> categories = _service.GetAllCategories();
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

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCategoryWindow();
            if (dialog.ShowDialog() == true)
            {
                string name = dialog.CategoryName;
                _service.AddCategory(name);
                LoadCategories();
            }
        }
    }
}