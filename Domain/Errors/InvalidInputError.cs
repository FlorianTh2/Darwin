namespace darwin.Domain.Errors;

public class InvalidInputError : DomainError
{
    public InvalidInputError(string name, object key)
        : base("Input [" + name + "] with key [" + key + "] ist not valid.") { }

    public InvalidInputError(string message, string name, object key)
        : base(message + " Input [" + name + "] with key [" + key + "] ist not valid.") { }
}