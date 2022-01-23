namespace hello_asp_identity.Contracts.V1.Responses;

public record PasswordResetByAdminResponse
{
    public string NewPassword { get; init; } = string.Empty;
}