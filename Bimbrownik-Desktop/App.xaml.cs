using System;
using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Views.Login;

namespace Bimbrownik_Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            new ResourceLanguageService().SetLanguage(LanguageCode.English);
        }
    }
}