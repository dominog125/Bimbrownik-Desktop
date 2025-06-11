using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Bimbrownik_Desktop.Models;
using Bimbrownik_Desktop.Services.Auth;
using Bimbrownik_Desktop.Services.Data.Dtos;
using Bimbrownik_Desktop.Services.Data.Mappers;
using Bimbrownik_Desktop.Utilities;

namespace Bimbrownik_Desktop.Services
{
    public class RecipeService
    {
        private static readonly string OfflineFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Offline");

        private static readonly string RecipeFilePath =
            Path.Combine(OfflineFolder, "recipes.json");

        private readonly AuthenticationApiClient _auth;

        public RecipeService(AuthenticationApiClient auth)
        {
            _auth = auth;

            if (!Directory.Exists(OfflineFolder))
                Directory.CreateDirectory(OfflineFolder);

            if (!File.Exists(RecipeFilePath))
                File.WriteAllText(RecipeFilePath, "[]");
        }

        public async Task<List<Recipe>> LoadRecipesAsync()
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                try
                {
                    var recipeDtos = await _auth.GetRecipesAsync();
                    foreach (var dto in recipeDtos)
                    {
                        Console.WriteLine($"DTO: {dto.Id} {dto.Name} {dto.Title} {dto.Description} {dto.Author}");
                    }

                    var recipesFromApi = recipeDtos
                        .Select(RecipeMapper.FromPostResponseDto)
                        .ToList();

                    SaveAllRecipes(recipesFromApi);
                }
                catch { }
            }

            return LoadRecipesOffline();
        }

        private List<Recipe> LoadRecipesOffline()
        {
            if (!File.Exists(RecipeFilePath))
                return new List<Recipe>();

            try
            {
                var json = File.ReadAllText(RecipeFilePath);
                return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
            }
            catch
            {
                return new List<Recipe>();
            }
        }

        public void SaveAllRecipes(List<Recipe> recipes)
        {
            var json = JsonSerializer.Serialize(recipes, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            File.WriteAllText(RecipeFilePath, json, Encoding.UTF8);
            Debug.WriteLine($"[RecipeService] Zapisano {recipes.Count} przepisów do offline JSON.");
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                var dto = RecipeMapper.ToAddRecipeDto(recipe);
                await _auth.AddRecipeAsync(dto);
            }
            else
            {
                var all = LoadRecipesOffline();
                all.Add(recipe);
                SaveAllRecipes(all);
            }
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                var dto = RecipeMapper.ToUpdateRecipeDto(recipe);
                await _auth.UpdateRecipeAsync(dto);
            }
            else
            {
                var all = LoadRecipesOffline();
                var index = all.FindIndex(r => r.Id == recipe.Id);
                if (index >= 0)
                {
                    all[index] = recipe;
                    SaveAllRecipes(all);
                }
            }
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            if (NetworkHelper.IsInternetAvailable())
            {
                await _auth.DeleteRecipeAsync(id);
            }
            else
            {
                var all = LoadRecipesOffline();
                all.RemoveAll(r => r.Id == id);
                SaveAllRecipes(all);
            }
        }
    }
}
