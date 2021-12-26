using hello_asp_identity.Data.EntityConfigurations;
using hello_asp_identity.Domain;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Data;

public class DataContext : IdentityDbContext<
    AppUser,
    AppRole,
    string,
    IdentityUserClaim<string>,
    AppUserRole,
    IdentityUserLogin<string>,
    IdentityRoleClaim<string>,
    IdentityUserToken<string>
    >
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    private readonly ICurrentUserService _currentUserService;
    private IDateTimeService _dateTimeService;

    public DataContext(DbContextOptions<DataContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        : base(options)
    {
        this._currentUserService = currentUserService;
        this._dateTimeService = dateTimeService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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