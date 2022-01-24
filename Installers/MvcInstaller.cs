using FluentValidation.AspNetCore;
using hello_asp_identity.Data;
using hello_asp_identity.Filters;

namespace hello_asp_identity.Installers;

public static class MvcInstaller
{
    public static void InstallMvc(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        })
        .AddFluentValidation(options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<Program>();
        });

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();
    }
}