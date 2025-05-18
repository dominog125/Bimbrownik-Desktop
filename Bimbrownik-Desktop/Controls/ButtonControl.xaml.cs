using System.Windows.Controls;
using System.Windows;

namespace Bimbrownik_Desktop.Controls;

public partial class ButtonControl : UserControl
{
    public ButtonControl() => InitializeComponent();

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string),
            typeof(ButtonControl), new PropertyMetadata(string.Empty));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly RoutedEvent CommandTriggeredEvent =
        EventManager.RegisterRoutedEvent(nameof(CommandTriggered),
            RoutingStrategy.Bubble, typeof(RoutedEventHandler),
            typeof(ButtonControl));

    public event RoutedEventHandler CommandTriggered
    {
        add => AddHandler(CommandTriggeredEvent, value);
        remove => RemoveHandler(CommandTriggeredEvent, value);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
        => RaiseEvent(new RoutedEventArgs(CommandTriggeredEvent));
}