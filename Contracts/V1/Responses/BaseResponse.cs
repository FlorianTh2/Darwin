namespace hello_asp_identity.Contracts.V1.Responses;

///
/// Base class for Response, Response<T>, PagedResponse, ErrorResponse
///
public record BaseResponse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}