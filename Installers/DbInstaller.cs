using darwin.Data;
using darwin.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace darwin.Installers;

public static class DbInstaller
{
    public static void InstallDb(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();

        string connectionString = config.GetSection(ConnectionStringOptions.SectionName).Get<ConnectionStringOptions>().PostgresLocal;
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