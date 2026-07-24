using Base.Infrastructure.Database.EntityFramework.Context.Authentication;
using Base.Infrastructure.Database.EntityFramework.Entity;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Context;

public class BaseDbContext :  DbContext
{
    public DbSet<TestTableEntity>  TestTable { get; set; }
    
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<RoleEntity> Roles { get; set; }

    public DbSet<UserRoleEntity> UserRoles { get; set; }

    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new RefreshTokenConfiguration());
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUserId();
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy= GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = GetCurrentUserId();
                    break;
            }
        }
    }
    
    private int GetCurrentUserId()
    {
        var userId = 0;
        // int.TryParse(UserContext.ExternalId, out userId);
        return userId;
    }
}