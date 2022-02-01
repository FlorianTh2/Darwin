namespace hello_asp_identity.Domain.Errors;

public class InvalidInputError : DomainError
{
    public InvalidInputError(string name, object key)
        : base("Input [" + name + "] with key [" + key + "] ist not valid.") { }
}