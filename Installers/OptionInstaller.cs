using hello_asp_identity.Options;

namespace hello_asp_identity.Installers;

public static class OptionInstaller
{
    public static void InstallOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
        services.Configure<AdminUserSeedOptions>(config.GetSection(AdminUserSeedOptions.SectionName));
    }
}