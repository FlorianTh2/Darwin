using hello_asp_identity.Data;

namespace hello_asp_identity.Installers;

public static class DbInstaller
{
    public static void InstallDb(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        // string ConnectionString = Configuration.GetConnectionString("PersonalWebsiteBackendContextPostgre");
        services.AddDbContext<DbContext>(options =>
            options.UseNpgsql("ConnectionString"));
    }
}