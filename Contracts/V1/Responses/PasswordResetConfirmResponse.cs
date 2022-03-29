namespace Darwin.Contracts.V1.Responses;

public record PasswordResetConfirmResponse
{
    public string Description { get; init; } = string.Empty;
}