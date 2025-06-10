using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using Bimbrownik_Desktop.Models;

namespace Bimbrownik_Desktop.Services
{
    public class CategoryService
    {
        private static readonly string OfflineFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Offline");

        private static readonly string CategoryFilePath =
            Path.Combine(OfflineFolder, "categories.json");

        public CategoryService()
        {
            if (!Directory.Exists(OfflineFolder))
                Directory.CreateDirectory(OfflineFolder);

            if (!File.Exists(CategoryFilePath))
                File.WriteAllText(CategoryFilePath, "[]");
        }

        public List<DrinkCategory> GetAllCategories()
        {
            if (!File.Exists(CategoryFilePath))
                return new List<DrinkCategory>();

            try
            {
                var json = File.ReadAllText(CategoryFilePath);
                return JsonSerializer.Deserialize<List<DrinkCategory>>(json) ?? new List<DrinkCategory>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania kategorii: " + ex.Message, "Błąd JSON");
                return new List<DrinkCategory>();
            }
        }

        public void SaveAllCategories(List<DrinkCategory> categories)
        {
            var json = JsonSerializer.Serialize(categories, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(CategoryFilePath, json, Encoding.UTF8);
        }

        public void AddCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            var all = GetAllCategories();

            if (all.Exists(c => c.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Taka kategoria już istnieje.", "Błąd");
                return;
            }

            all.Add(new DrinkCategory
            {
                Id = all.Count + 1,
                Name = name.Trim()
            });

            SaveAllCategories(all);
        }
    }
}