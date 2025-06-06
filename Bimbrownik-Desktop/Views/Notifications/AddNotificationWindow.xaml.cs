using System.Windows;

namespace Bimbrownik_Desktop.Views.Notifications;

public partial class AddNotificationWindow : Window
{
    public string NotificationMessage => MessageInput.Text.Trim();

    public AddNotificationWindow()
    {
        InitializeComponent();
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NotificationMessage))
        {
            MessageBox.Show("Treść powiadomienia nie może być pusta.");
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