using System.Threading;
using System.Threading.Tasks;
using Bimbrownik_Desktop.Services.Auth.Dtos;

namespace Bimbrownik_Desktop.Services.Auth;

public interface AuthenticationApiClient
{
    Task<LoginResult> LoginAsync(LoginRequestDto request, CancellationToken ct = default);
}