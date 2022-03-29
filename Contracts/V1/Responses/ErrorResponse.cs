namespace Darwin.Contracts.V1.Responses;

public record ErrorResponse<T> : Response
{
    public List<T> Errors { get; set; } = new List<T>();

    public ErrorResponse() { }
    public ErrorResponse(List<T> errors)
    {
        Errors = errors;
    }
}