using Base.Domain.Models.Formalities;
using Base.Domain.Repositories.Formalities;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;
using Base.Infrastructure.Database.EntityFramework.Extensions.Formalities;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Base.Infrastructure.Database.EntityFramework.Repositories.Formalities;

public class RequestTypeRepository : GenericRepository<RequestTypeEntity>, IRequestTypeRepository
{
    private readonly BaseDbContext _dbContext;


    public RequestTypeRepository(BaseDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<RequestTypeModel>
        CreateAsync(
            RequestTypeModel model)
    {
        var entity =
            model.ToEntity();

        var created =
            await base.CreateAsync(entity);

        return created.ToModel();
    }


    public async Task<RequestTypeModel>
        UpdateAsync(
            RequestTypeModel model)
    {
        var entity =
            model.ToEntity();

        var updated =
            await base.UpdateAsync(entity);

        return updated.ToModel();
    }


    public new async Task<RequestTypeModel?>
        GetByIdAsync(
            int id)
    {
        var entity =
            await _dbContext.Set<RequestTypeEntity>()
                .FindAsync(id);

        return entity?.ToModel();
    }


    public async Task<bool>
        ExistsByNameAsync(
            string name)
    {
        return await _dbContext
            .Set<RequestTypeEntity>()
            .AnyAsync(x =>
                x.Name == name &&
                x.IsActive);
    }


    public async Task<List<RequestTypeModel>>
        GetAllActiveAsync()
    {
        var entities =
            await _dbContext
                .Set<RequestTypeEntity>()
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .ToListAsync();

        return entities
            .Select(x => x.ToModel())
            .ToList();
    }
}