namespace Darwin.Contracts.HealthChecks;

public record HealthCheck
{
    public string Status { get; init; } = string.Empty;

    public string Component { get; init; } = string.Empty;

    public string? Description { get; init; } = string.Empty;
}