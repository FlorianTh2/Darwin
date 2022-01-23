namespace hello_asp_identity.Domain.Results;

public class AuthenticationResult : Result
{
    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}