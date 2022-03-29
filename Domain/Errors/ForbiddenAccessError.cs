namespace darwin.Domain.Errors;

public class ForbiddenAccessError : DomainError
{
    // unauthorized = not logged in
    // forbidden access = logged in but does not have permission
    public ForbiddenAccessError(string message) : base(message) { }
}