namespace hello_asp_identity.Domain.Errors;

public class NoChangesSavedDatabaseError : DomainError
{
    public NoChangesSavedDatabaseError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] was changed in database.") { }
}