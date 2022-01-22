using hello_asp_identity.Data;
using hello_asp_identity.Entities;
using hello_asp_identity.Options;
using hello_asp_identity.Provider;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Installers;

public static class IdentityInstaller
{
    public static void InstallIdentity(this IServiceCollection services, IConfiguration config)
    {
        var accountSecruityOptions = config.GetSection(AccountSecruityOptions.SectionName).Get<AccountSecruityOptions>();

        var emailProviderName = "ActiveEmailConfirmationProvider";
        var passwordResetTokenProviderName = "AppResetPasswordTokenProvider";

        services.AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            // .AddDefaultTokenProviders()
            .AddTokenProvider<AppEmailConfirmationTokenProvider<AppUser>>(emailProviderName)
            .AddTokenProvider<AppResetPasswordTokenProvider<AppUser>>(passwordResetTokenProviderName);

        services.Configure<IdentityOptions>(a =>
        {
            a.Password.RequiredLength = accountSecruityOptions.PasswordLength;
            a.Password.RequireNonAlphanumeric = true;
            a.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            a.Lockout.AllowedForNewUsers = true;
            a.Lockout.MaxFailedAccessAttempts = 5;
            a.SignIn.RequireConfirmedEmail = true;
            a.Tokens.EmailConfirmationTokenProvider = emailProviderName;
            a.Tokens.PasswordResetTokenProvider = passwordResetTokenProviderName;
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