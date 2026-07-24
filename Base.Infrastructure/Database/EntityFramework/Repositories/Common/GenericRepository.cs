using Base.Domain.Repositories.Common;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Common;

public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity: BaseEntity, IIdentifiable
{
    private readonly BaseDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    protected GenericRepository(
        BaseDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity) 
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        // var entityEntry = _dbSet.Update(entity);
        // await _dbContext.SaveChangesAsync();
        // return entityEntry.Entity;
        var trackedEntity = await _dbSet.FindAsync(entity.Id);

        if (trackedEntity is null)
            throw new Exception("Entity not found");

        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

        await _dbContext.SaveChangesAsync();

        return trackedEntity;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public async Task<bool> DeleteHardAsync(int id) 
    {
        var del = await _dbSet.FindAsync(id);
        if (del is null) return false;
        
        _dbSet.Remove(del);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}