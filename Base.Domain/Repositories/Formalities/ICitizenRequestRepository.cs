using Base.Domain.Models.Formalities;
using Base.Domain.Repositories.Common;
using Base.Domain.Responses;

namespace Base.Domain.Repositories.Formalities;

public interface ICitizenRequestRepository : IGenericRepository<CitizenRequestModel>
{
    Task<int> CountAsync();

    // Task<List<CitizenRequestModel>> GetPagedAsync(int pageNumber, int pageSize);

    Task<bool> ExistsRequestTypeAsync(int requestTypeId);
    
    Task<PagedResult<CitizenRequestModel>> GetPagedAsync(int page, int pageSize);
}