using FluentValidation.Results;

namespace hello_asp_identity.Domain.Errors;

public class ValidationError : DomainError
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationError()
    : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationError(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}