namespace hello_asp_identity.Domain.Results;

public class Result
{
    public bool Success { get; set; } = false;

    public IEnumerable<string> Errors { get; set; } = new List<string>();
}

public class Result<T> : Result
{
    public T? Data { get; init; }

    public Result() { }

    public Result(T data)
    {
        Success = true;
        Data = data;
    }
}