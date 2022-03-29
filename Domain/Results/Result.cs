using darwin.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace darwin.Domain.Results;

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

    public bool Succeeded() { return Success; }

    public bool Failed() { return !Success; }

    public List<DomainError>? GetConsecutiveErrors()
    {
        return Errors.Skip(1).ToList();
    }

    public Result AddConsecutiveError(string methodName)
    {
        Errors.Add(new ConsecutiveError(methodName));
        return this;
    }

    // public Result<U> CastToGeneric<U>()
    // {
    //     return new Result<U> { Success = Success, Errors = Errors };
    // }

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

    public static Result Fail(Result result)
    {
        return new Result()
        {
            Success = false,
            Errors = result.Errors
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

    public T0 Then<T0>(Func<Result, T0> func)
    {
        return func(this);
    }

    public static Result<T> Fail<T>(DomainError error)
    {
        return new Result<T>()
        {
            Success = false,
            Errors = new List<DomainError>() { error }
        };
    }

    public static Result<U> Fail<U>(Result result)
    {
        return new Result<U>()
        {
            Success = false,
            Errors = result.Errors
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

    public new Result<T> AddConsecutiveError(string methodName)
    {
        Errors.Add(new ConsecutiveError(methodName));
        return this;
    }

    public T0 Then<T0>(Func<Result<T>, T0> func)
    {
        return func(this);
    }

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