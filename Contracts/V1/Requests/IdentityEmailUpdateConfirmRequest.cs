namespace Darwin.Contracts.V1.Requests;

public record IdentityEmailUpdateConfirmRequest(
    Guid UserId,
    string EmailConfirmationToken
);