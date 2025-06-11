using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Data.Dtos;
using Bimbrownik_Desktop.Utilities;

namespace Bimbrownik_Desktop.Services;

public class CategoryService
{
    private static readonly string OfflineFolder =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Offline");

    private static readonly string CategoryFilePath =
        Path.Combine(OfflineFolder, "categories.json");

    private readonly AuthenticationApiClient _api;

    public CategoryService(AuthenticationApiClient api)
    {
        _api = api;

        if (!Directory.Exists(OfflineFolder))
            Directory.CreateDirectory(OfflineFolder);

        if (!File.Exists(CategoryFilePath))
            File.WriteAllText(CategoryFilePath, "[]");
    }

    public async Task<List<CategoryDto>> LoadCategoriesAsync()
    {
        if (NetworkHelper.IsInternetAvailable())
        {
            try
            {
                var categories = await _api.GetCategoriesAsync();
                SaveAllCategories(categories);
            }
            catch
            { 
            }
        }

        return LoadCategoriesOffline();
    }

    public List<CategoryDto> LoadCategoriesOffline()
    {
        if (!File.Exists(CategoryFilePath))
            return new List<CategoryDto>();

        try
        {
            var json = File.ReadAllText(CategoryFilePath);
            return JsonSerializer.Deserialize<List<CategoryDto>>(json) ?? new List<CategoryDto>();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd podczas wczytywania kategorii: " + ex.Message, "Błąd JSON");
            return new List<CategoryDto>();
        }
    }

    public void SaveAllCategories(IEnumerable<CategoryDto> categories)
    {
        var json = JsonSerializer.Serialize(categories, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(CategoryFilePath, json, Encoding.UTF8);
    }

    public async Task AddCategoryAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;

        if (NetworkHelper.IsInternetAvailable())
        {
            await _api.AddCategoryAsync(name);
        }
        else
        {
            var list = LoadCategoriesOffline();
            if (list.Any(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Taka kategoria już istnieje.", "Błąd");
                return;
            }

            list.Add(new CategoryDto(Guid.NewGuid(), name));
            SaveAllCategories(list);
        }
    }
}