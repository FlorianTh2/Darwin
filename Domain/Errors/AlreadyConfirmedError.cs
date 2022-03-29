namespace darwin.Domain.Errors;

public class AlreadyConfirmedError : DomainError
{
    public AlreadyConfirmedError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] is already confirmed.") { }
}