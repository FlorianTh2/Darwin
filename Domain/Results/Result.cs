using hello_asp_identity.Domain.Errors;

namespace hello_asp_identity.Domain.Results;

public class Result
{
    public bool Success { get; set; }

    // public DomainError? Error { get; set; }

    // first element is origin error (the first appeared error)
    // last element is last/most outer error of error call chain
    public List<DomainError> Errors { get; set; } = new List<DomainError>();

    public Result() { }

    public DomainError? GetOriginError()
    {
        return Errors.FirstOrDefault();
    }

    public List<DomainError>? GetConsecutiveErrors()
    {
        return Errors.Skip(1).ToList();
    }

    public Result AddConsecutiveError(DomainError error)
    {
        Errors.Add(error);
        return this;
    }

    public static Result Ok()
    {
        return new Result() { Success = true };
    }

    public static Result Fail(DomainError error)
    {
        return new Result()
        {
            Success = false,
            Errors = new List<DomainError>() { error }
        };
    }

    public static implicit operator Result(DomainError error)
    {
        return new Result()
        {
            Success = false,
            Errors = new List<DomainError>() { error }
        };
    }

    public static Result<T> Fail<T>(DomainError error)
    {
        return new Result<T>()
        {
            Success = false,
            Errors = new List<DomainError>() { error }
        };
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>() { Success = true, Value = value };
    }

}

public class Result<T> : Result
{

    public T Value { get; init; } = default!;

    public Result() : base() { }

    public static implicit operator Result<T>(DomainError error)
    {
        return new Result<T>()
        {
            Success = false,
            Errors = new List<DomainError>() { error }
        };
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>()
        {
            Success = true,
            Value = value
        };
    }
}