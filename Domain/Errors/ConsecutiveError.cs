namespace Darwin.Domain.Errors;

public class ConsecutiveError : DomainError
{
    public ConsecutiveError(string methodName)
        : base("As a result of a previos error the action [" + methodName + "] can not continue.") { }
}