namespace hello_asp_identity.Domain.Exceptions.Identity;

public class IdentityException : DomainException
{
    public IdentityException() { }
    public IdentityException(string message) : base(message) { }

    public IdentityException(string message, Exception innerException)
    : base(message, innerException) { }
}