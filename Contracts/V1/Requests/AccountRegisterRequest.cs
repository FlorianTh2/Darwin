namespace hello_asp_identity.Contracts.V1.Requests;

public record AccountRegisterRequest(
    string UserName,
    string Email,
    string Password,
    string PasswordConfirm
);