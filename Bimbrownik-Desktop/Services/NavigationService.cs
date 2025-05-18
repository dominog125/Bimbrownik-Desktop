using System;
using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Views.Login;
using Bimbrownik_Desktop.Views.Recipes;
using Bimbrownik_Desktop.Views.Categories;
using Bimbrownik_Desktop.Views.Notifications;
using Bimbrownik_Desktop.Views.Statistics;

namespace Bimbrownik_Desktop.Services
{
    public class NavigationService
    {
        private static NavigationService? _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        private ContentControl? _contentArea;

        private NavigationService()
        {
        }

        public void Initialize(ContentControl contentArea)
        {
            _contentArea = contentArea;
        }

        public void NavigateTo(UserControl view)
        {
            if (_contentArea == null)
                throw new InvalidOperationException("NavigationService not initialized.");

            _contentArea.Content = view;
        }

        public void Logout(UserControl caller)
        {
            var login = new LoginWindow();
            login.Show();

            Application.Current.MainWindow = login;
            Window.GetWindow(caller)?.Close();
        }
    }
}