namespace Darwin.Contracts.V1.Responses;

public record EmailUpdateResponse
{
    public string Description { get; init; } = string.Empty;
}