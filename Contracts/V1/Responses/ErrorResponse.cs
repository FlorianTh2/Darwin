namespace hello_asp_identity.Contracts.V1.Responses;

public record ErrorResponse<T> : Response
{
    public IEnumerable<T> Errors { get; set; } = new List<T>();

    public ErrorResponse() { }
    public ErrorResponse(IEnumerable<T> errors)
    {
        Errors = errors;
    }
}