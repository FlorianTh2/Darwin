namespace hello_asp_identity.Domain.Results;

public class Result
{
    public bool Success { get; set; } = false;

    public List<string>? Errors { get; set; } = new List<string>();

    public Result() { }

    // public static Result Ok() => _ok;

    // public static Result Ok(string message)
    // {
    //     return new(false, message, null);
    // }
    // public static Result Ok(string message, string details)
    // {
    //     return new(false, message, details);
    // }

    // public static Result Fail(string message, string details = null)
    // {
    //     return new(true, message, details);
    // }

    // public static Result Fail(string message, IEnumerable<ValidationFailure> failures)
    // {
    //     return new(true, message, string.Empty, failures);
    // }

    // public static Result<T> Fail<T>(string message, string details = null)
    // {
    //     return new(default, true, message, details);
    // }

    // public static Result<T> Fail<T>(string message, IEnumerable<ValidationFailure> failures)
    // {
    //     return new(default, true, message, string.Empty, failures);
    // }

    // public static Result<T> Fail<T>(string message, string details, IEnumerable<ValidationFailure> failures)
    // {
    //     return new(default, true, message, details, failures);
    // }

    // public static Result<T> Ok<T>(T value)
    // {
    //     return new(value, false, string.Empty, string.Empty);
    // }

    // public static Result<T> Ok<T>(T value, string message, string details = null)
    // {
    //     return new(value, false, message, details);
    // }
}

public class Result<T> : Result
{

    public T Data { get; init; } = default!;

    public Result() : base() { }
}