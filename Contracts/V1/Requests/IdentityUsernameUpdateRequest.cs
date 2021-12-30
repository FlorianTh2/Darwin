namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityUsernameUpdateRequest(
    string OldUsername,
    string NewUsername
);