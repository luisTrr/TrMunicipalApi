using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Authentication;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Authentication;

public class RoleRepository
    : GenericRepository<RoleEntity>, IRoleRepository
{
    private readonly BaseDbContext _dbContext;

    public RoleRepository(
        BaseDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic Crud

    public async Task<RoleModel> CreateAsync(
        RoleModel model)
    {
        var entity = model.ToEntity();

        var created = await base.CreateAsync(entity);

        return created.ToModel();
    }


    public async Task<RoleModel> UpdateAsync(
        RoleModel model)
    {
        var entity = model.ToEntity();

        var updated = await base.UpdateAsync(entity);

        return updated.ToModel();
    }


    public new async Task<RoleModel?> GetByIdAsync(
        int id)
    {
        var entity = await _dbContext.Roles
            .FindAsync(id);

        return entity?.ToModel();
    }

    #endregion


    #region Authentication

    public async Task<RoleModel?> GetByNameAsync(
        string name)
    {
        var entity = await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r =>
                r.Name == name);

        return entity?.ToModel();
    }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _dbContext.Roles
            .AnyAsync(r =>
                r.Name == name);
    }

    #endregion
}