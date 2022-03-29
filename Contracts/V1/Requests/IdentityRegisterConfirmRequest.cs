namespace Darwin.Contracts.V1.Requests;

public record IdentityRegisterConfirmRequest(
    Guid UserId,
    string Token
);