namespace hello_asp_identity.Contracts.V1.Requests;

public record IdentityEmailUpdateRequest(
    string OldEmail,
    string UnConfirmedEmail
);