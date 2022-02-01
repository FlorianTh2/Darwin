namespace hello_asp_identity.Domain.Exceptions.Identity;

public class UserAlreadyExistsException : IdentityException
{
    public UserAlreadyExistsException() { }
    public UserAlreadyExistsException(string message) : base(message) { }

    public UserAlreadyExistsException(string message, Exception innerException)
    : base(message, innerException) { }
}