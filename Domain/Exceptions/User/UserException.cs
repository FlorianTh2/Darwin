namespace hello_asp_identity.Domain.Exceptions.User;

public class UserException : DomainException
{
    public UserException() { }
    public UserException(string message) : base(message) { }

    public UserException(string message, Exception innerException)
    : base(message, innerException) { }
}