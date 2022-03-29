namespace Darwin.Contracts.V1.Requests;

public record IdentityPasswordUpdateRequest
{
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordConfirm { get; set; } = null!;
}