namespace Darwin.Contracts.V1.Requests;

public record IdentityRegisterRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DOB { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordConfirm { get; set; } = null!;
}