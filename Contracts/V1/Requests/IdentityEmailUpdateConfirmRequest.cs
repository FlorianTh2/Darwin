namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityEmailUpdateConfirmRequest(
    int UserId,
    string EmailConfirmationToken
);