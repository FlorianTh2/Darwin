using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Results;

namespace hello_asp_identity.Services;

public interface IIdentityService
{
    Task<Result<RegisterResult>> RegisterAsync(string username, string email, string password, DateTime dbo);
    Task<Result<AuthResult>> RegisterConfirmAsync(int userId, string token);
    Task<Result<AuthResult>> LoginAsync(string username, string password);
    Task<Result<AuthResult>> RefreshTokenAsync(string accessToken, string refreshToken);
    Task<Result<PasswordResetResult>> PasswordResetAsync(string email);

    // maybe important in cases where user cant reset his password himself
    // email of user who needs a password reset
    Task<Result<PasswordResetByAdminResult>> PasswordResetByAdminAsync(string email);
    Task<Result> PasswordResetConfirmAsync(int userId, string token, string password);
    Task<Result> PasswordUpdateAsync(int userId, string password, string newPassword);
    Task<Result> UsernameUpdateAsync(int userId, string newUsername);
    Task<Result<EmailResetResult>> EmailUpdateAsync(int userId, string oldEmail, string unConfirmedEmail);
    Task<Result> EmailUpdateConfirmAsync(int userId, string token);
    Task<Result<bool>> DeleteUserByIdAsync(int userId);
    Task<Result<bool>> IsInRoleAsync(int userId, string role);
    Task<Result> InvalidateRefreshtokensAsync(int userId);
}