namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityLoginRequest
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}