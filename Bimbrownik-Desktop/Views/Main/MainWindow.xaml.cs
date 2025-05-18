using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.MainMenu;

namespace Bimbrownik_Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService.Instance.Initialize(ContentArea);
            NavigationService.Instance.NavigateTo(new MainMenuView());
        }
    }
}