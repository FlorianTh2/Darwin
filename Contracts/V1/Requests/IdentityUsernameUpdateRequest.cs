namespace darwin.Contracts.V1.Requests;

public record IdentityUsernameUpdateRequest(
    string Username,
    string NewUsername
);