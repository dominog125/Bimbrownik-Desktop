using System.Windows.Controls;
using System.Windows;
using Bimbrownik_Desktop.Views.Login;
using Bimbrownik_Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bimbrownik_Desktop.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        public static NavigationService Instance => _instance ??= new NavigationService();

        private ContentControl _contentArea;
        private ServiceProvider? _serviceProvider;

        public void Initialize(ContentControl contentArea)
        {
            _contentArea = contentArea;
        }

        public void InitializePlaceholder(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo(UserControl view)
        {
            _contentArea.Content = view;
        }

        public void Logout(UserControl caller)
        {
            if (_serviceProvider == null)
                throw new InvalidOperationException("NavigationService is not initialized with ServiceProvider.");

            var vm = _serviceProvider.GetRequiredService<LoginViewModel>();
            var login = new LoginWindow(vm);

            login.Show();
            Application.Current.MainWindow = login;

            Window.GetWindow(caller)?.Close();
        }
    }
}