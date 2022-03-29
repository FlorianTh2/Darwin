namespace darwin.Domain.Errors;

public class LockedOutError : DomainError
{
    public LockedOutError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] is locked out of the system.") { }
}