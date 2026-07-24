using Base.Domain.Models.Authentication;
using Base.Domain.Repositories.Common;

namespace Base.Domain.Repositories.Authentication;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<UserModel?> GetByEmailAsync(string email);

    Task<UserModel?> GetByUsernameAsync(string username);

    Task<bool> ExistsByEmailAsync(string email);

    Task<bool> ExistsByUsernameAsync(string username);

    Task<IEnumerable<string>> GetRolesAsync(int userId);
}