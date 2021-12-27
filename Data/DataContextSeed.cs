using hello_asp_identity.Domain;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Data;

public static class DataContextSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, SeedOptions seedOptions)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

        await SeedDefaultUserAsync(userManager, roleManager, seedOptions.AdminOptions);

    }

    public static async Task SeedDefaultUserAsync(UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager, AdminOptions adminOptions)
    {
        var administratorRole = new AppRole();
        administratorRole.Name = adminOptions.AdminRoleName;

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new AppUser()
        {
            UserName = adminOptions.Username,
            Email = adminOptions.Email
        };

        if (userManager.Users.All(u => u.Email != administrator.Email))
        {
            await userManager.CreateAsync(administrator, adminOptions.Password);
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }
}