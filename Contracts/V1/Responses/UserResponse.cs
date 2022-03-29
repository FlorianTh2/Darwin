namespace Darwin.Contracts.V1.Responses;

public record UserResponse(
    Guid Id,
    string Username,
    string Email,
    int Age,
    bool EmailConfirmed,
    bool Suspended,
    string CreatedOn,
    string UpdatedOn,
    string UpdaterId
);