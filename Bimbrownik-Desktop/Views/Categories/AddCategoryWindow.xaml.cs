using System.Windows;

namespace Bimbrownik_Desktop.Views.Categories;

public partial class AddCategoryWindow : Window
{
    public string CategoryName => CategoryNameBox.Text.Trim();

    public AddCategoryWindow()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CategoryName))
        {
            MessageBox.Show("Nazwa kategorii nie może być pusta.",
                            "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}