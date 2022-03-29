using Darwin.Services;

namespace Darwin.Installers;

public static class ProjectServiceInstaller
{
    public static void InstallProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IUriService>(provider =>
        {
            var request = provider.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?
                .Request;
            var absoluteUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent(), "/");
            return new UriService(absoluteUri);
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddTransient<IDateTimeService, DateTimeService>();

    }
}