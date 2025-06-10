using System.Windows;
using System.Windows.Controls;

namespace Bimbrownik_Desktop.Views.Layouts;

public partial class SectionHeader : UserControl
{
    public static readonly DependencyProperty HeaderTextProperty =
        DependencyProperty.Register(nameof(HeaderText), typeof(string), typeof(SectionHeader));

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public SectionHeader()
    {
        InitializeComponent();
    }
}