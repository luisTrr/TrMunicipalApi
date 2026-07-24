using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Database.EntityFramework.Context.Authentication;

public class RefreshTokenConfiguration
    : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(
        EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable(
            "RefreshToken",
            schema: "SEC");

        builder.HasComment(
            "Tokens utilizados para renovar la autenticación.");

        builder.HasKey(rt => rt.Id);

        builder.Property(rt => rt.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(rt => rt.UserId)
            .HasColumnName("userId")
            .IsRequired();

        builder.Property(rt => rt.Token)
            .HasColumnName("token")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(rt => rt.ExpiresAt)
            .HasColumnName("expiresAt")
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .HasColumnName("revokedAt");

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(rt => rt.Token)
            .IsUnique();
    }
}