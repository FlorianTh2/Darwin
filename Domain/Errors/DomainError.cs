namespace darwin.Domain.Errors;

public class DomainError
{
    // message should contain email
    // e.g. $"There is already an existing permission group with name: {name}"
    public string? Message { get; }

    public DomainError(string message)
    {
        Message = message;
    }
}