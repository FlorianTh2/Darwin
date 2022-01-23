namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityPasswordResetConfirmRequest(
    int UserId,
    string PasswordResetConfirmationToken,
    string password,
    string passwordConfirm
);
