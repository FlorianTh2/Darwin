namespace hello_asp_identity.Contracts.V1.Responses;

public record Response() : BaseResponse;

public record Response<T> : Response
{
    public T Data { get; init; }

    public Response() { }

    public Response(T data)
    {
        Data = Data;
    }
}