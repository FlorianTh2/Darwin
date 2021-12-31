using hello_asp_identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hello_asp_identity.Data.EntityConfigurations;

public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable(nameof(AppUser), AppDbContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.HasMany(a => a.UserRoles).WithOne(a => a.User).HasForeignKey(a => a.UserId).IsRequired();
    }
}