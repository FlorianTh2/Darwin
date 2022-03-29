namespace darwin.Domain.Errors;

public class NoChangesSavedDatabaseError : DomainError
{
    public NoChangesSavedDatabaseError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] was changed in database.") { }
}