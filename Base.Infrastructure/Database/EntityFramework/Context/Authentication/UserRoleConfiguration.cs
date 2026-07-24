using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Database.EntityFramework.Context.Authentication;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRole", schema: "SEC");

        builder.HasComment(
            "Relación entre usuarios y roles.");

        builder.HasKey(ur => ur.Id);

        builder.Property(ur => ur.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(ur => ur.UserId)
            .HasColumnName("userId")
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .HasColumnName("roleId")
            .IsRequired();

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ur => new
            {
                ur.UserId,
                ur.RoleId
            })
            .IsUnique();
    }
}