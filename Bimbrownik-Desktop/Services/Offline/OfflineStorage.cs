using System.Collections.Generic;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Data.Dtos;

public interface OfflineStorage
{
    Task SaveRecipesAsync(IEnumerable<RecipeDto> recipes);
    Task SaveCategoriesAsync(IEnumerable<CategoryDto> categories);
    Task SaveStatisticsAsync(StatisticsDto statistics);
    Task SaveAllAsync(
        IEnumerable<RecipeDto> recipes,
        IEnumerable<CategoryDto> categories,
        StatisticsDto statistics);
    Task<IEnumerable<RecipeDto>> LoadRecipesAsync();
    Task<IEnumerable<CategoryDto>> LoadCategoriesAsync();
    Task<StatisticsDto?> LoadStatisticsAsync();
}