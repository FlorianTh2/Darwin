using darwin.Domain;
using darwin.Domain.Results;

namespace darwin.Services;

public interface IIdentityService
{
    Task<Result<RegisterResult>> RegisterAsync(string username, string email, string password, DateTime dbo);
    Task<Result<AuthResult>> RegisterConfirmAsync(Guid userId, string token);
    Task<Result<AuthResult>> LoginAsync(string username, string password);
    Task<Result<AuthResult>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result<PasswordResetResult>> PasswordResetAsync(string email);

    // maybe important in cases where user cant reset his password himself
    // email of user who needs a password reset
    Task<Result<PasswordResetByAdminResult>> PasswordResetByAdminAsync(string email);
    Task<Result> PasswordResetConfirmAsync(Guid userId, string token, string password);
    Task<Result> PasswordUpdateAsync(Guid userId, string password, string newPassword);
    Task<Result> UsernameUpdateAsync(Guid userId, string newUsername);
    Task<Result<EmailResetResult>> EmailUpdateAsync(Guid userId, string oldEmail, string unConfirmedEmail);
    Task<Result> EmailUpdateConfirmAsync(Guid userId, string token);
    Task<Result> DeleteUserByIdAsync(Guid userId);
    Task<Result<bool>> IsInRoleAsync(Guid userId, string role);
    Task<Result> InvalidateRefreshtokensAsync(Guid userId);
}