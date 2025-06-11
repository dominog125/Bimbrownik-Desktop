using System.Threading;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth.Dtos;
using Bimbrownik_Desktop.Services.Data.Dtos;

namespace Bimbrownik_Desktop.Services.Auth;

public interface AuthenticationApiClient
{
    Task<LoginResult> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
    public abstract Task<List<RecipeDto>> GetRecipesAsync(CancellationToken cancellationToken = default);
    public abstract Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    public abstract Task<StatisticsDto> GetStatisticsAsync(CancellationToken cancellationToken = default);

    Task AddRecipeAsync(AddRecipeDto dto, CancellationToken ct = default);
    Task UpdateRecipeAsync(UpdateRecipeDto dto, CancellationToken ct = default);
    Task DeleteRecipeAsync(Guid id, CancellationToken ct = default);

    Task AddCategoryAsync(string name);
}