namespace hello_asp_identity.Domain.Exceptions.User;

public class UserNotFoundException : UserException
{
    public UserNotFoundException() { }
    public UserNotFoundException(string message) : base(message) { }

    public UserNotFoundException(string message, Exception innerException)
    : base(message, innerException) { }
}