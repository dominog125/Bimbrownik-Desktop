using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.MainMenu;

namespace Bimbrownik_Desktop.Views.Main
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService.Instance.Initialize(ContentArea);
            NavigationService.Instance.NavigateTo(new MainMenuView());
        }

        public void GoToMainMenu()
        {
            ContentArea.Content = new MainMenuView();
        }
    }
}