using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Results;

namespace hello_asp_identity.Services;

public interface IIdentityService
{
    Task<RegisterResult> RegisterAsync(string username, string email, string password, DateTime dbo);
    Task<AuthenticationResult> RegisterConfirmAsync(int userId, string token);
    Task<AuthenticationResult> LoginAsync(string username, string password);
    Task<AuthenticationResult> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<PasswordResetResult> PasswordResetAsync(string email);

    // maybe important in cases where user cant reset his password himself
    // email of user who needs a password reset
    Task<PasswordResetByAdminResult> PasswordResetByAdminAsync(string email);
    Task<Result> PasswordResetConfirmAsync(int userId, string token, string password);
    Task<Result> PasswordUpdateAsync(int userId, string password, string newPassword);
    Task<Result> UsernameUpdateAsync(int userId, string password, string newPassword);
    Task<EmailResetResult> EmailUpdateAsync(int userId, string password, string newPassword);
    Task<Result> EmailUpdateConfirmAsync(string password, string newPassword);
    Task<Result> DeleteUserByIdAsync(int userId);
    Task<bool> IsInRoleAsync(int userId, string role);
}