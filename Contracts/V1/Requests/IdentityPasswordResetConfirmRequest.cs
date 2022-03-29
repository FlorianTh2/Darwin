namespace darwin.Contracts.V1.Requests;

public record IdentityPasswordResetConfirmRequest(
    Guid UserId,
    string PasswordResetConfirmationToken,
    string password,
    string passwordConfirm
);
