namespace hello_asp_identity.Domain.Results;

public class EmailResetResult : Result
{
    public string? CallbackUrl { get; set; }
}