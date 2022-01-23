namespace hello_asp_identity.Contracts.V1.Responses;

public record AuthResponse
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}