namespace hello_asp_identity.Contracts.HealthChecks;

public record HealthCheck
{
    public string Status { get; init; }

    public string Component { get; init; }

    public string Description { get; init; }
}