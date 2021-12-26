using hello_asp_identity.Data;
using hello_asp_identity.Options;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Installers;

public static class DbInstaller
{
    public static void InstallDb(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();

        string connectionString = config.GetSection(ConnectionStringOptions.SectionName).Get<ConnectionStringOptions>().Local;
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(connectionString));
    }
}