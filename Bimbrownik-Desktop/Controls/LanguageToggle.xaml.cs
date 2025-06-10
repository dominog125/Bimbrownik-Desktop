using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Bimbrownik_Desktop.Services;

namespace Bimbrownik_Desktop.Controls
{
    public partial class LanguageToggle : UserControl
    {
        private static readonly ResourceLanguageService languageService = new();

        public LanguageToggle()
        {
            InitializeComponent();
            UpdateFlag();
        }

        private void FlagImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var current = languageService.CurrentLanguage;
            var next = current == LanguageCode.English ? LanguageCode.Polish : LanguageCode.English;

            languageService.SetLanguage(next);
            UpdateFlag();
        }

        private void UpdateFlag()
        {
            var current = languageService.CurrentLanguage;

            var imagePath = current == LanguageCode.English
                ? "pack://application:,,,/Images/flag_us.png"
                : "pack://application:,,,/Images/flag_pl.png";

            FlagImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }
    }
}