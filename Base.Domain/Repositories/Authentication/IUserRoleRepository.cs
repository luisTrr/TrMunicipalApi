using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Common;

namespace Base.Domain.Repositories.Authentication;

public interface IUserRoleRepository : IGenericRepository<UserRoleModel>
{
    Task<IEnumerable<RoleModel>> GetRolesByUserIdAsync(int userId);

    Task<bool> ExistsAsync(int userId, int roleId);

    Task<bool> DeleteByUserIdAsync(int userId);
}