namespace darwin.Contracts.V1.Requests;

public record UserUpdateRequest
{
    /// <summary>
    /// expecting utc-time in iso 8601 format
    /// </summary>
    public string DOB { get; set; } = string.Empty;
}