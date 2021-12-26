using hello_asp_identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hello_asp_identity.Data.EntityConfigurations;

public class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(nameof(AppRole).ToLower(), DataContext.DEFAULT_SCHEMA);

        builder.HasMany(a => a.UserRoles).WithOne(a => a.Role).HasForeignKey(a => a.RoleId).IsRequired();

        // builder.HasMany(a => a.AppRoles).WithMany(a => a.AppUsers).UsingEntity<AppUserRole>(
        //     a => a
        //         .HasOne(b => b.Role)
        //         .WithMany(b => b.UserRoles)
        //         .HasForeignKey(b => b.RoleId),
        //     a => a.HasOne(b => b.User).WithMany(b => b.UserRoles).HasForeignKey(b => b.UserId),
        //     a =>
        //     {
        //         a.HasKey(b => new { b.UserId, b.RoleId });
        //     }
        // );

        // builder.HasKey(a => new { a.UserId, a.RoleId });

        // builder.HasOne(a => a.User).WithMany(a => a.UserRoles).HasForeignKey(a => a.UserId);

        // builder.HasOne(a => a.Role).WithMany(a => a.UserRoles).HasForeignKey(a => a.RoleId);
    }
}