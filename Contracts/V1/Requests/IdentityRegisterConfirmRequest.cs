namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityRegisterConfirmRequest(
    Guid UserId,
    string Token
);