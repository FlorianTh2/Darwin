using FluentValidation.AspNetCore;

namespace hello_asp_identity.Installers;

public static class MvcInstaller
{
    public static void InstallMvc(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            // options.Filters.Add<ValidationFilter>;
            return;
        })
        .AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<Program>();
        });
    }
}