namespace darwin.Contracts.V1.Requests;

public record IdentityPasswordResetByAdminRequest
{
    // email of user whos password has to be resetted
    public string Email { get; set; } = null!;
}