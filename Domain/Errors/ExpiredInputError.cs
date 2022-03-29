namespace darwin.Domain.Errors;

public class ExpiredInputError : DomainError
{
    public ExpiredInputError(string name, object key)
        : base("Input [" + name + "] with key [" + key + "] is expired.") { }
}