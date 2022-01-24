namespace hello_asp_identity.Domain.Results;

public class Result
{
    public bool Success { get; set; } = false;

    public List<string>? Errors { get; set; } = new List<string>();

    public Result() { }
    public Result(List<string> errors)
    {
        Errors = errors;
    }
}

public class Result<T> : Result
{
#nullable disable

    public T Data { get; init; }

#nullable enable

    public Result() : base() { }

    public Result(T data) : base()
    {
        Data = data;
    }
}