namespace hello_asp_identity.Domain.Errors;

public class ValidationError : DomainError
{
    public ValidationError(string name, object key)
        : base("") { }
}