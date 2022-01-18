using Serilog;

namespace hello_asp_identity.Installers;
public static class SerilogInstaller
{
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
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Program>(); });
    }
}