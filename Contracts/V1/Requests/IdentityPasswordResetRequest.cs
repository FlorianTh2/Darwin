namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityPasswordResetRequest
{
    public string Email { get; set; } = null!;
}