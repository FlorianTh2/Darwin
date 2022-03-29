using darwin.Contracts.HealthChecks;
using darwin.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Serilog;

namespace darwin.Installers;

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

        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;
        app.UseRequestLocalization(options.Value);

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

    }
}