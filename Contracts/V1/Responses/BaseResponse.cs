namespace hello_asp_identity.Contracts.V1.Responses;

public record BaseResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}