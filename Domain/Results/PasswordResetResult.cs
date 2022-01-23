namespace hello_asp_identity.Domain.Results;

public class PasswordResetResult : Result
{
    public string? CallbackUrl { get; set; }
}