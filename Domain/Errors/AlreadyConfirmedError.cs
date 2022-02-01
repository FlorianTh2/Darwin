namespace hello_asp_identity.Domain.Errors;

public class AlreadyConfirmedError : DomainError
{
    public AlreadyConfirmedError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] is already confirmed.") { }
}