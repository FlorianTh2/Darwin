namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityRefreshToken(
    string Token,
    string RefreshToken
);