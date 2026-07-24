using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Common;

namespace Base.Domain.Repositories.Authentication;

public interface IRoleRepository : IGenericRepository<RoleModel>
{
    Task<RoleModel?> GetByNameAsync(string name);

    Task<bool> ExistsByNameAsync(string name);
}