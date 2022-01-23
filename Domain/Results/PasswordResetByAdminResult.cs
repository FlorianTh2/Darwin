namespace hello_asp_identity.Domain.Results;

public class PasswordResetByAdminResult : Result
{
    public string? NewPassword { get; set; }
}