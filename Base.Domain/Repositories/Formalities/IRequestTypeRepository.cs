using Base.Domain.Models.Formalities;
using Base.Domain.Repositories.Common;

namespace Base.Domain.Repositories.Formalities;

public interface IRequestTypeRepository : IGenericRepository<RequestTypeModel>
{
    Task<bool> ExistsByNameAsync(string name);

    Task<List<RequestTypeModel>> GetAllActiveAsync();
}