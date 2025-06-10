using System.Windows.Controls;
using System.Windows;
using Bimbrownik_Desktop.Views.Login;

namespace Bimbrownik_Desktop.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        private ContentControl _contentArea;

        public void Initialize(ContentControl contentArea)
        {
            _contentArea = contentArea;
        }

        public void NavigateTo(UserControl view)
        {
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