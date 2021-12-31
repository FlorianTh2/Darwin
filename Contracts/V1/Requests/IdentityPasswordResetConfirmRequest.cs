namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityPasswordResetConfirmRequest(
    Guid UserId,
    string PasswordResetConfirmationToken
);