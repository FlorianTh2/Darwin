using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Installers;

public static class IdentityInstaller
{
    public static void InstallIdentity(this IServiceCollection services, IConfiguration config)
    {
        var accountSecruityOptions = config.GetSection(AccountSecruityOptions.SectionName).Get<AccountSecruityOptions>();

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(a =>
        {
            a.Password.RequiredLength = accountSecruityOptions.PasswordLength;
            a.Password.RequireNonAlphanumeric = true;
            a.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            a.Lockout.AllowedForNewUsers = true;
            a.Lockout.MaxFailedAccessAttempts = 5;
        });

        // services.AddAuthorization(options =>
        // {
        //     // options.AddPolicy("ProjectViewer", builder => builder.RequireClaim("project.view", "true"));
        //     options.AddPolicy("MustWorkForDotCom", policy =>
        //     {
        //         policy.AddRequirements(new WorksForCompanyRequirement(".com"));
        //     } );
        //
        // // await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "projects.view"));
        // });
    }
}