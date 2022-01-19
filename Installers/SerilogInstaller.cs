using Serilog;
using Serilog.Core;
using Serilog.Events;
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
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .ReadFrom.Configuration(context.Configuration);
            });
    }
}