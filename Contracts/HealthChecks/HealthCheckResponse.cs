namespace hello_asp_identity.Contracts.HealthChecks;

public record HealthCheckResponse
{
    public string Status { get; init; }
    public IEnumerable<HealthCheck> Checks { get; init; }
    public TimeSpan Duration { get; init; }
}