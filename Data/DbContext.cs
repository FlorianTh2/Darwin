using hello_asp_identity.Data.EntityConfigurations;
using hello_asp_identity.Domain;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hello_asp_identity.Data;

public class DbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public const string DEFAULT_SCHEMA = "dbo";
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    private readonly ICurrentUserService _currentUserService;
    private IDateTimeService _dateTimeService;

    public DbContext(DbContextOptions<DbContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        : base(options)
    {
        this._currentUserService = currentUserService;
        this._dateTimeService = dateTimeService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}