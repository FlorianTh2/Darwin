namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityEmailUpdateConfirmRequest(
    Guid UserId,
    string EmailConfirmationToken
);