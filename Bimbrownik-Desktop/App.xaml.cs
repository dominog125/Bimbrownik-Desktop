using System;
using System.Net.Http;
using System.Windows;
using Bimbrownik_Desktop.Services;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Auth.Session;
using Bimbrownik_Desktop.Services.Offline;
using Bimbrownik_Desktop.ViewModels;
using Bimbrownik_Desktop.Views.Categories;
using Bimbrownik_Desktop.Views.Login;
using Bimbrownik_Desktop.Views.Recipes;
using Microsoft.Extensions.DependencyInjection;

namespace Bimbrownik_Desktop
{
    public partial class App : Application
    {
        private ServiceProvider? serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://app-bimbrownik-eupl-dev-001-drdmbud9b2fvfqe8.canadacentral-01.azurewebsites.net")
            });

            services.AddSingleton<TokenStorage>();
            services.AddSingleton<OfflineStorage, FileOfflineStorage>();
            services.AddSingleton<AuthenticationApiClient, HttpAuthenticationApiClient>();
            services.AddSingleton<RecipeService>();
            services.AddSingleton<CategoryService>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<RecipesView>();
            services.AddTransient<CategoriesView>();

            serviceProvider = services.BuildServiceProvider();

            new ResourceLanguageService().SetLanguage(LanguageCode.English);

            NavigationService.Instance.InitializePlaceholder(serviceProvider);

            var loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
        }
    }
}