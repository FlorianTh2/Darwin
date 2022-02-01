namespace hello_asp_identity.Domain.Errors;

public class NotFoundError : DomainError
{
    public NotFoundError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] was not found.") { }
}