using System.Windows;
using System.Windows.Controls;
using Bimbrownik_Desktop.Views.Main;

namespace Bimbrownik_Desktop.Views.Layouts
{
    public partial class BackToMenuArrow : UserControl
    {
        public BackToMenuArrow()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.GoToMainMenu();
            }
        }
    }
}