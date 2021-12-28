namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityLoginRequest(
    string Username,
    string Password
);