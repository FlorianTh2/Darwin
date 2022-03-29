namespace darwin.Contracts.V1.Responses;

public record PasswordResetByAdminResponse
{
    public string NewPassword { get; init; } = string.Empty;
}