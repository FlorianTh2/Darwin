using hello_asp_identity.Domain;

namespace hello_asp_identity.Services;

public interface IIdentityService
{
    // weitere methodensignaturen hinzufügen: im grunde 1 foreach controller-endpoint

    Task<AuthenticationResult> RegisterAsync(string username, string email, string password, DateTime dbo);
    Task<AuthenticationResult> LoginAsync(string username, string password);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

    Task<bool> DeleteUserByIdAsync(int userId);

    Task<bool> IsInRoleAsync(int userId, string role);
}