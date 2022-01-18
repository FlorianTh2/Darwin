namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityRegisterConfirmRequest(
    int UserId,
    string RegisterConfirmationToken
);