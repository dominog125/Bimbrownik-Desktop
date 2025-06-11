using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Offline;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Services.Offline;

public class FileOfflineStorage : OfflineStorage
{
    private readonly string basePath = Path.Combine("Data", "Offline");

    public FileOfflineStorage()
    {
        Directory.CreateDirectory(basePath);
    }

    public async Task SaveRecipesAsync(IEnumerable<RecipeDto> recipes)
    {
        var path = GetPath("recipes.json");
        var json = JsonSerializer.Serialize(recipes, GetJsonOptions());
        await File.WriteAllTextAsync(path, json);
    }

    public async Task SaveCategoriesAsync(IEnumerable<CategoryDto> categories)
    {
        var path = GetPath("categories.json");
        var json = JsonSerializer.Serialize(categories, GetJsonOptions());
        await File.WriteAllTextAsync(path, json);
    }

    public async Task SaveStatisticsAsync(StatisticsDto statistics)
    {
        var path = GetPath("statistics.json");
        var json = JsonSerializer.Serialize(statistics, GetJsonOptions());
        await File.WriteAllTextAsync(path, json);
    }

    public async Task SaveAllAsync(
        IEnumerable<RecipeDto> recipes,
        IEnumerable<CategoryDto> categories,
        StatisticsDto statistics)
    {
        await SaveRecipesAsync(recipes);
        await SaveCategoriesAsync(categories);
        await SaveStatisticsAsync(statistics);
    }

    public async Task<IEnumerable<RecipeDto>> LoadRecipesAsync()
    {
        var path = GetPath("recipes.json");
        if (!File.Exists(path)) return Array.Empty<RecipeDto>();

        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<IEnumerable<RecipeDto>>(json, GetJsonOptions()) ?? Array.Empty<RecipeDto>();
    }

    public async Task<IEnumerable<CategoryDto>> LoadCategoriesAsync()
    {
        var path = GetPath("categories.json");
        if (!File.Exists(path)) return Array.Empty<CategoryDto>();

        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(json, GetJsonOptions()) ?? Array.Empty<CategoryDto>();
    }

    public async Task<StatisticsDto?> LoadStatisticsAsync()
    {
        var path = GetPath("statistics.json");
        if (!File.Exists(path)) return null;

        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<StatisticsDto>(json, GetJsonOptions());
    }

    private string GetPath(string fileName) => Path.Combine(basePath, fileName);

    private JsonSerializerOptions GetJsonOptions() => new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}