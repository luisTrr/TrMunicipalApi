using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Authentication;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Authentication;

public class UserRoleRepository
    : GenericRepository<UserRoleEntity>,
      IUserRoleRepository
{
    private readonly BaseDbContext _dbContext;

    public UserRoleRepository(
        BaseDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic Crud

    public async Task<UserRoleModel> CreateAsync(
        UserRoleModel model)
    {
        var entity = model.ToEntity();

        var created = await base.CreateAsync(entity);

        return created.ToModel();
    }


    public async Task<UserRoleModel> UpdateAsync(
        UserRoleModel model)
    {
        var entity = model.ToEntity();

        var updated = await base.UpdateAsync(entity);

        return updated.ToModel();
    }


    public new async Task<UserRoleModel?> GetByIdAsync(
        int id)
    {
        var entity = await _dbContext.UserRoles
            .FindAsync(id);

        return entity?.ToModel();
    }

    #endregion


    #region User Roles

    public async Task<IEnumerable<RoleModel>>
        GetRolesByUserIdAsync(
            int userId)
    {
        var entities = await _dbContext.UserRoles
            .AsNoTracking()
            .Where(ur =>
                ur.UserId == userId &&
                ur.Role.IsActive)
            .Include(ur =>
                ur.Role)
            .Select(ur =>
                ur.Role)
            .ToListAsync();

        return entities
            .Select(role =>
                role.ToModel());
    }


    public async Task<bool> ExistsAsync(
        int userId,
        int roleId)
    {
        return await _dbContext.UserRoles
            .AnyAsync(ur =>
                ur.UserId == userId &&
                ur.RoleId == roleId);
    }


    public async Task<bool> DeleteByUserIdAsync(
        int userId)
    {
        var entities = await _dbContext.UserRoles
            .Where(ur =>
                ur.UserId == userId)
            .ToListAsync();

        if (!entities.Any())
            return false;

        _dbContext.UserRoles.RemoveRange(entities);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}