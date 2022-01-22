namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityRegisterRequest(
    string UserName,
    string Email,
    string DOB,
    string Password,
    string PasswordConfirm
);