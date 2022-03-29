namespace Darwin.Contracts.V1.Responses;

public record PasswordResetResponse
{
    public string Description { get; init; } = string.Empty;
}