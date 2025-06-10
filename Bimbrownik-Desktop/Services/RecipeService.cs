using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.Services
{
    public class RecipeService
    {
        private static readonly string OfflineFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Offline");

        private static readonly string RecipeFilePath =
            Path.Combine(OfflineFolder, "recipes.json");

        public RecipeService()
        {
            if (!Directory.Exists(OfflineFolder))
                Directory.CreateDirectory(OfflineFolder);

            if (!File.Exists(RecipeFilePath))
                File.WriteAllText(RecipeFilePath, "[]");
        }

        public List<Recipe> LoadRecipes()
        {
            if (!File.Exists(RecipeFilePath))
                return new List<Recipe>();

            try
            {
                var json = File.ReadAllText(RecipeFilePath);
                return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania przepisów: " + ex.Message, "Błąd JSON");
                return new List<Recipe>();
            }
        }

        public void SaveAllRecipes(List<Recipe> recipes)
        {
            var json = JsonSerializer.Serialize(recipes, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(RecipeFilePath, json, Encoding.UTF8);
        }
    }
}