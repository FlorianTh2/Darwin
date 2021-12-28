using hello_asp_identity.Data;
using hello_asp_identity.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hello_asp_identity.Installers;

public static class DbInstaller
{
    public static void InstallDb(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();

        string connectionString = config.GetSection(ConnectionStringOptions.SectionName).Get<ConnectionStringOptions>().Local;
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                a =>
                {
                    a.MigrationsHistoryTable(HistoryRepository.DefaultTableName, AppDbContext.DEFAULT_SCHEMA);
                    a.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                }));
    }
}