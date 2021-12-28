namespace hello_asp_identity.Contracts.V1.Requests;

public class AccountRegisterConfirmRequest
{
    public string Confirmation_token { get; set; }
}