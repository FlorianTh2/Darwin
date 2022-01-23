namespace hello_asp_identity.Contracts.V1.Responses;

public record UsernameUpdateResponse
{
    public string Description { get; init; } = string.Empty;
}