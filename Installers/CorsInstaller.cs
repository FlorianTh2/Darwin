namespace Darwin.Installers;
public static class CorsInstaller
{
    public static void InstallCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder
                    // allow any origin
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    // allow credentials
                    .AllowCredentials());

        });
    }
}