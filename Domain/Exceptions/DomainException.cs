namespace hello_asp_identity.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() { }

    // message should contain email
    // e.g. $"There is already an existing permission group with name: {name}"
    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException)
        : base(message, innerException) { }
}