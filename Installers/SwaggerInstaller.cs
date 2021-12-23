namespace hello_asp_identity.Installers;

public static class SwaggerInstaller
{
    public static void InstallSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}