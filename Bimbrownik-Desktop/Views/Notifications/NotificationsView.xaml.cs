using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Notifications;

namespace Bimbrownik_Desktop.Views.Notifications;

public partial class NotificationsView : UserControl
{
    private readonly NotificationService _service = new();

    public NotificationsView()
    {
        InitializeComponent();
        LoadNotifications();
    }

    private void LoadNotifications()
    {
        NotificationList.Children.Clear();
        var all = _service.LoadAll();

        foreach (var notification in all)
        {
            var block = new TextBlock
            {
                Text = $"{notification.Message}\n{notification.CreatedAt:yyyy-MM-dd HH:mm}",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 15),
                FontSize = 14
            };

            NotificationList.Children.Add(block);
        }
    }

    private void AddNotification_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new AddNotificationWindow();
        if (dialog.ShowDialog() == true)
        {
            var message = dialog.NotificationMessage;
            _service.AddNotification(message);
            LoadNotifications();
        }
    }
}