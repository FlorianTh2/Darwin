namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityPasswordResetConfirmRequest(
    string Reset_token
);