using hello_asp_identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hello_asp_identity.Data.EntityConfigurations;

public class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(nameof(AppRole), DataContext.DEFAULT_SCHEMA);

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.HasMany(a => a.UserRoles).WithOne(a => a.Role).HasForeignKey(a => a.RoleId).IsRequired();
    }
}