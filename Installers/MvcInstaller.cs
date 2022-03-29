using Darwin.Data;
using Darwin.Filters;
using FluentValidation.AspNetCore;

namespace Darwin.Installers;

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
        })
        .AddDataAnnotationsLocalization();

        services.AddHealthChecks()
            .AddDbContextCheck<AppDbContext>();
    }
}