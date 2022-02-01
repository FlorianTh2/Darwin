namespace hello_asp_identity.Domain.Errors;

public class ForbiddenAccessError : DomainError
{
    public ForbiddenAccessError(string message) : base(message)
    {
    }
}