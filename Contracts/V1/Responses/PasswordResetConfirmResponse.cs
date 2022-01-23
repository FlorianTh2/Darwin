namespace hello_asp_identity.Contracts.V1.Responses;

public record PasswordResetConfirmResponse
{
    public string Description { get; init; } = string.Empty;
}