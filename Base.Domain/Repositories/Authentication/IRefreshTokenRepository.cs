using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Common;

namespace Base.Domain.Repositories.Authentication;

public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenModel>
{
    Task<RefreshTokenModel?> GetByTokenAsync(string token);

    Task<IEnumerable<RefreshTokenModel>> GetByUserIdAsync(int userId);

    Task<bool> RevokeAsync(string token);

    Task<bool> DeleteExpiredAsync();
}