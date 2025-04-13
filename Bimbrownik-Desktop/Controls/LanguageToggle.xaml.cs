using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Bimbrownik_Desktop.Services;

namespace Bimbrownik_Desktop.Controls
{
    public partial class LanguageToggle : UserControl
    {
        private readonly LanguageService languageService = new ResourceLanguageService();
        private LanguageCode currentLanguage = LanguageCode.English;

        public LanguageToggle()
        {
            InitializeComponent();
            languageService.SetLanguage(currentLanguage);
            UpdateFlag();
        }

        private void FlagImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentLanguage = currentLanguage == LanguageCode.English ? LanguageCode.Polish : LanguageCode.English;
            languageService.SetLanguage(currentLanguage);
            UpdateFlag();
        }

        private void UpdateFlag()
        {
            var imagePath = currentLanguage == LanguageCode.English
                ? "pack://application:,,,/Images/flag_us.png"
                : "pack://application:,,,/Images/flag_pl.png";

            FlagImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
        }
    }
}