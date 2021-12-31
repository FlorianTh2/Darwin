using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Installers;

public static class CreateMigrateSeedDbInstaller
{
    public static async void InstallCreateMigrateSeedDb(this WebApplication app)
    {
        // migrate database schema + seed database
        using (var serviceScope = app.Services.CreateScope())
        {

            // migrate + create (if not created yet) database
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var seedOptions = app.Configuration.GetSection(SeedOptions.SectionName).Get<SeedOptions>();
            await AppDbContextSeed.SeedAsync(serviceScope.ServiceProvider, seedOptions);
        }
    }
}