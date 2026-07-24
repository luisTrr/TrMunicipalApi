using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Authentication;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Authentication;

public class UserRepository
    : GenericRepository<UserEntity>, IUserRepository
{
    private readonly BaseDbContext _dbContext;

    public UserRepository(
        BaseDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic Crud

    public async Task<UserModel> CreateAsync(
        UserModel model)
    {
        var entity = model.ToEntity();

        var created = await base.CreateAsync(entity);

        return created.ToModel();
    }

    public async Task<UserModel> UpdateAsync(
        UserModel model)
    {
        var entity = model.ToEntity();

        var updated = await base.UpdateAsync(entity);

        return updated.ToModel();
    }

    public new async Task<UserModel?> GetByIdAsync(
        int id)
    {
        var entity = await _dbContext.Users
            .FindAsync(id);

        return entity?.ToModel();
    }

    #endregion


    #region Authentication

    public async Task<UserModel?> GetByEmailAsync(
        string email)
    {
        var entity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.Email == email);

        return entity?.ToModel();
    }


    public async Task<UserModel?> GetByUsernameAsync(
        string username)
    {
        var entity = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.Username == username);

        return entity?.ToModel();
    }


    public async Task<bool> ExistsByEmailAsync(
        string email)
    {
        return await _dbContext.Users
            .AnyAsync(u =>
                u.Email == email);
    }


    public async Task<bool> ExistsByUsernameAsync(
        string username)
    {
        return await _dbContext.Users
            .AnyAsync(u =>
                u.Username == username);
    }


    public async Task<IEnumerable<string>> GetRolesAsync(
        int userId)
    {
        return await _dbContext.UserRoles
            .AsNoTracking()
            .Where(ur =>
                ur.UserId == userId &&
                ur.Role.IsActive)
            .Select(ur =>
                ur.Role.Name)
            .ToListAsync();
    }

    #endregion
}