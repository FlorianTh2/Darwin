namespace hello_asp_identity.Domain.Results;

public class AuthenticationResult
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}