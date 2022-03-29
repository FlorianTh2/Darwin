using Darwin.Helpers;
using Darwin.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Darwin.Installers;

public static class OptionInstaller
{
    public static void InstallOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
        services.Configure<SeedOptions>(config.GetSection(SeedOptions.SectionName));
        services.Configure<AccountSecruityOptions>(config.GetSection(AccountSecruityOptions.SectionName));
    }
}