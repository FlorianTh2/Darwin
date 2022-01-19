using Serilog;
using Serilog.Extensions.Hosting;

namespace hello_asp_identity.Installers;
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
                    .MinimumLevel.Override(
                        "Microsoft.EntityFrameworkCore.Database.Command",
                        Serilog.Events.LogEventLevel.Warning)
                    // .Enrich.WithProperty("app", context.HostingEnvironment.ApplicationName)
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .ReadFrom.Configuration(context.Configuration);
            });
    }
}