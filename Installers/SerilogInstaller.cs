using darwin.Contracts.V1.Requests;
using darwin.Entities;
using darwin.Extensions;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Json;

namespace darwin.Installers;
public static class SerilogInstaller
{
    public static ReloadableLogger BootstrapSerilog()
    {
        return new LoggerConfiguration()
            .WriteTo.Console()
            // CreateBootstrapLogger() sets up Serilog so that the initial logger configuration
            // (which writes only to Console), can be swapped out later in the initialization process,
            // once the web hosting infrastructure is available.
            .CreateBootstrapLogger();
    }

    public static void InstallSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder
            .UseSerilog((context, config) =>
            {
                config
                    .ReadFrom.Configuration(context.Configuration)
                    .MinimumLevel.Override(
                        "Microsoft.EntityFrameworkCore.Database.Command",
                        Serilog.Events.LogEventLevel.Warning)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("app", context.HostingEnvironment.ApplicationName)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .ObfuscateSensitiveData()
                    .WriteTo.Console(new JsonFormatter());

            });
    }
}