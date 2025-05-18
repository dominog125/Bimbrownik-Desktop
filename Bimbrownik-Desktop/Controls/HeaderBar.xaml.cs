using System.Windows;
using System.Windows.Controls;

namespace Bimbrownik_Desktop.Controls;

public partial class HeaderBar : UserControl
{
    public HeaderBar() => InitializeComponent();

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string),
            typeof(HeaderBar), new PropertyMetadata(string.Empty));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}