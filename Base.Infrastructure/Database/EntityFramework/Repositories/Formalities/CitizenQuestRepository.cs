using Base.Domain.Models.Formalities;
using Base.Domain.Repositories.Formalities;
using Base.Domain.Responses;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;
using Base.Infrastructure.Database.EntityFramework.Extensions.Formalities;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Formalities;

public class CitizenRequestRepository : GenericRepository<CitizenRequestEntity>, ICitizenRequestRepository
{
    private readonly BaseDbContext _dbContext;
    
    public CitizenRequestRepository(
        BaseDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic CRUD

    public async Task<CitizenRequestModel> CreateAsync(CitizenRequestModel model)
    {
        var entity = model.ToEntity();

        var created = await base.CreateAsync(entity);

        return created.ToModel();
    }


    public async Task<CitizenRequestModel> UpdateAsync(CitizenRequestModel model)
    {
        var entity = model.ToEntity();

        var updated = await base.UpdateAsync(entity);

        return updated.ToModel();
    }


    public new async Task<CitizenRequestModel?> GetByIdAsync(int id)
    {
        var entity =
            await _dbContext
                .Set<CitizenRequestEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    !x.IsDeleted);

        return entity?.ToModel();
    }

    #endregion


    #region Pagination

    public async Task<int> CountAsync()
    {
        return await _dbContext
            .Set<CitizenRequestEntity>()
            .CountAsync(x =>
                !x.IsDeleted);
    }


    public async Task<PagedResult<CitizenRequestModel>> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbContext
                .Set<CitizenRequestEntity>()
                .AsNoTracking()
                .Where(x => !x.IsDeleted);


        var totalItems = await query.CountAsync();


        var entities = await query
                .OrderByDescending(x => x.RegisteredAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


        var items =
            entities
                .Select(x =>
                    x.ToModel())
                .ToList();


        return new PagedResult<CitizenRequestModel>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    #endregion


    public async Task<bool> ExistsRequestTypeAsync(int requestTypeId)
    {
        return await _dbContext
            .Set<RequestTypeEntity>()
            .AnyAsync(x => x.Id == requestTypeId && x.IsActive);
    }
}