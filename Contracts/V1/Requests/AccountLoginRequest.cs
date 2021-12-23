namespace hello_asp_identity.Contracts.V1.Requests;

public record AccountLoginRequest(
    string Username,
    string Password
);