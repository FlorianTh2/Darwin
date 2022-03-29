namespace darwin.Contracts.V1.Requests;

public record IdentityRefreshToken(
    string Token,
    string RefreshToken
);