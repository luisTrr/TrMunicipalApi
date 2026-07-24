using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Database.EntityFramework.Context.Formalities;

public class RequestTypeConfiguration
    : IEntityTypeConfiguration<RequestTypeEntity>
{
    public void Configure(
        EntityTypeBuilder<RequestTypeEntity> builder)
    {
        builder.ToTable(
            "RequestType",
            schema: "CIT");

        builder.HasComment(
            "Tipos de trámites ciudadanos.");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("isActive")
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}