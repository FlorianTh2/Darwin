using hello_asp_identity.Helpers;
using hello_asp_identity.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace hello_asp_identity.Installers;

public static class OptionInstaller
{
    public static void InstallOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
        services.Configure<SeedOptions>(config.GetSection(SeedOptions.SectionName));
        services.Configure<AccountSecruityOptions>(config.GetSection(AccountSecruityOptions.SectionName));
    }
}