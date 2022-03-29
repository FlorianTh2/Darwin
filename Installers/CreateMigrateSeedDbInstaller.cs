using darwin.Data;
using darwin.Domain;
using darwin.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace darwin.Installers;

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