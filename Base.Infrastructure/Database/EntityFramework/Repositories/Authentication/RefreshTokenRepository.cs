using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Authentication;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;
using Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Authentication;

public class RefreshTokenRepository
    : GenericRepository<RefreshTokenEntity>,
      IRefreshTokenRepository
{
    private readonly BaseDbContext _dbContext;

    public RefreshTokenRepository(
        BaseDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic Crud

    public async Task<RefreshTokenModel> CreateAsync(
        RefreshTokenModel model)
    {
        var entity = model.ToEntity();

        var created = await base.CreateAsync(entity);

        return created.ToModel();
    }


    public async Task<RefreshTokenModel> UpdateAsync(
        RefreshTokenModel model)
    {
        var entity = model.ToEntity();

        var updated = await base.UpdateAsync(entity);

        return updated.ToModel();
    }


    public new async Task<RefreshTokenModel?> GetByIdAsync(
        int id)
    {
        var entity = await _dbContext.RefreshTokens
            .FindAsync(id);

        return entity?.ToModel();
    }

    #endregion


    #region Refresh Token

    public async Task<RefreshTokenModel?>
        GetByTokenAsync(
            string token)
    {
        var entity = await _dbContext.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt =>
                rt.Token == token);

        return entity?.ToModel();
    }


    public async Task<IEnumerable<RefreshTokenModel>>
        GetByUserIdAsync(
            int userId)
    {
        var entities = await _dbContext.RefreshTokens
            .AsNoTracking()
            .Where(rt =>
                rt.UserId == userId)
            .ToListAsync();

        return entities
            .Select(token =>
                token.ToModel());
    }


    public async Task<bool> RevokeAsync(
        string token)
    {
        var entity = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt =>
                rt.Token == token);

        if (entity is null)
            return false;

        entity.RevokedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }


    public async Task<bool> DeleteExpiredAsync()
    {
        var entities = await _dbContext.RefreshTokens
            .Where(rt =>
                rt.ExpiresAt <= DateTime.UtcNow)
            .ToListAsync();

        if (!entities.Any())
            return false;

        _dbContext.RefreshTokens
            .RemoveRange(entities);

        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}