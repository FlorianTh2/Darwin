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
    Guid,
    IdentityUserClaim<Guid>,
    AppUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>
    >
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<RefreshToken> RefreshTokens { get; set; }

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

        builder.ApplyConfiguration(new AppUserEntityTypeConfiguration());
        builder.ApplyConfiguration(new AppRoleEntityTypeConfiguration());
        builder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}