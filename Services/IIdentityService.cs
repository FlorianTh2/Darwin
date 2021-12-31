using hello_asp_identity.Domain;

namespace hello_asp_identity.Services;

public interface IIdentityService
{
    Task<AuthenticationResult> RegisterAsync(string username, string email, string password);
    Task<AuthenticationResult> LoginAsync(string username, string password);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
}