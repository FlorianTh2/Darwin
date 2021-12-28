namespace hello_asp_identity.Contracts.V1.Requests;

public class AccountPasswordResetConfirmRequest
{
    public string Reset_token { get; set; }
}