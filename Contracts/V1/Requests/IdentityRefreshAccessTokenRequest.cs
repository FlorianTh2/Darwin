namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityRefreshAccessTokenRequest
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}