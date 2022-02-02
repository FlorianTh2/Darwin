using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Enums;
using hello_asp_identity.Entities;
using hello_asp_identity.Extensions;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Data;

public static class AppDbContextSeed
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
        var rolesToStore = Enum.GetValues(typeof(Roles)).OfType<Roles>().ToList();

        var storedRoles = await roleManager.Roles.ToListAsync();

        rolesToStore.Select(async a =>
        {
            var roleName = a.ToString();
            if (storedRoles.All(r => r.Name != roleName))
            {
                await roleManager.CreateAsync(new AppRole(roleName));
            }
        });

        var administrator = new AppUser()
        {
            UserName = adminOptions.Username,
            Email = adminOptions.Email,
            DOB = new DateTime(1970, 1, 1).SetKind(DateTimeKind.Utc)
        };

        if (userManager.Users.All(u => u.Email != administrator.Email))
        {
            await userManager.CreateAsync(administrator, adminOptions.Password);
            await userManager.AddToRolesAsync(administrator, rolesToStore.Select((Roles a) => a.ToString()));
        }
    }
}