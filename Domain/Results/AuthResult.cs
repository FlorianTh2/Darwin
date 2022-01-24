namespace hello_asp_identity.Domain.Results;

public class AuthResult
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}