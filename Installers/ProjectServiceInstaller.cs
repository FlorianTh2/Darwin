using hello_asp_identity.Services;

namespace hello_asp_identity.Installers;

public static class ProjectServiceInstaller
{
    public static void InstallProjectServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
    }
}