using hello_asp_identity.Contracts.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace hello_asp_identity.Extensions;

public static class HealthCheckOptionsExtensions
{
    public static HealthCheckOptions SpecifyCustomResponseWriter(this HealthCheckOptions a)
    {
        a.ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";

            var response = new HealthCheckResponse
            {
                Checks = report.Entries.Select(a => new HealthCheck()
                {
                    Component = a.Key,
                    Status = a.Value.Status.ToString(),
                    Description = a.Value.Description
                }),
                Duration = report.TotalDuration
            };

            await context.Response.WriteAsJsonAsync(response);
        };
        return a;
    }
}