namespace Darwin.Domain.Errors;

public class AlreadyExistsError : DomainError
{
    public AlreadyExistsError(string name, object key)
        : base("Entity [" + name + "] with key [" + key + "] does already exist.") { }
}