using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Infrastructure.Database.EntityFramework.Context.Formalities;

public class CitizenRequestConfiguration
    : IEntityTypeConfiguration<CitizenRequestEntity>
{
    public void Configure(
        EntityTypeBuilder<CitizenRequestEntity> builder)
    {
        builder.ToTable(
            "CitizenRequest",
            schema: "CIT");

        builder.HasComment(
            "Solicitudes y trámites realizados por los ciudadanos.");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CitizenName)
            .HasColumnName("citizenName")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.RequestTypeId)
            .HasColumnName("requestTypeId")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(x => x.RegisteredAt)
            .HasColumnName("registeredAt")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Priority)
            .HasColumnName("priority")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("isDeleted")
            .IsRequired();

        builder.HasOne(x => x.RequestType)
            .WithMany(x => x.CitizenRequests)
            .HasForeignKey(x => x.RequestTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => x.Priority);

        builder.HasIndex(x => x.IsDeleted);
    }
}