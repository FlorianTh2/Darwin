using hello_asp_identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Installers;

public static class IdentityInstaller
{
    public static void InstallIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, AppRole>()
            .AddRoles<AppRole>()
            // to make entityFramework function with identity
            .AddEntityFrameworkStores<DataContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // options.Password getting ignored since definition at fluent-validation ist
        });
    }
}