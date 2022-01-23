namespace hello_asp_identity.Contracts.V1.Responses;

public record EmailUpdateConfirmResponse
{
    public string Description { get; init; } = string.Empty;
}