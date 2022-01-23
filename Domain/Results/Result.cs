namespace hello_asp_identity.Domain.Results;

public class Result
{
    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; } = new List<string>();
}