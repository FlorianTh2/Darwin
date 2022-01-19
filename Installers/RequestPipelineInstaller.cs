using hello_asp_identity.Contracts.HealthChecks;
using hello_asp_identity.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace hello_asp_identity.Installers;

public static class RequestPipelineInstaller
{
    public static void InstallRequestPipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("v1/swagger.json", "hello-asp-identity (V1)");
            });
        }
        app.UseSerilogRequestLogging();
        app.UseHealthChecks("/health", new HealthCheckOptions().SpecifyCustomResponseWriter());
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

    }
}