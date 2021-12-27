using hello_asp_identity.Domain;

namespace hello_asp_identity.Services;

public class AccountService : IAccountService
{
    public Task<AuthenticationResult> RegisterAsync(string username, string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> LoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
    {
        throw new NotImplementedException();
    }
}