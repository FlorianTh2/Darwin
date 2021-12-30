namespace hello_asp_identity.Contracts.V1.Responses;

public record AuthResponse(
    string Token,
    string RefreshToken
);