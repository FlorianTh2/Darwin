using System.Reflection;
using hello_asp_identity.Data.EntityConfigurations;
using hello_asp_identity.Entities;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Data;

public class AppDbContext : IdentityDbContext<
    AppUser,
    AppRole,
    int,
    IdentityUserClaim<int>,
    AppUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>
    >
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    private readonly ICurrentUserService _currentUserService;
    private IDateTimeService _dateTimeService;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService)
        : base(options)
    {
        this._currentUserService = currentUserService;
        this._dateTimeService = dateTimeService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema(DEFAULT_SCHEMA);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<IEntity<int>>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatorId = _currentUserService.UserId;
                    entry.Entity.CreatedOn = _dateTimeService.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdaterId = _currentUserService.UserId;
                    entry.Entity.UpdatedOn = _dateTimeService.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}