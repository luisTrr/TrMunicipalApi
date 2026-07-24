using Base.Domain.Models;
using Base.Domain.Repositories;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Entity;
using Base.Infrastructure.Database.EntityFramework.Extensions;
using Base.Infrastructure.Database.EntityFramework.Repositories.Common;

namespace Base.Infrastructure.Database.EntityFramework.Repositories;

public class TestTableRepository : GenericRepository<TestTableEntity>,ITestTableRepository
{
    private readonly BaseDbContext _dbContext;
    public TestTableRepository(BaseDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Basic Crud
    public Task<TestTableModel> CreateAsync(TestTableModel model)
    {
        var entity = model.ToEntity();
        return Task.FromResult(base.CreateAsync(entity).Result.ToModel());
    }
    public async Task<TestTableModel> UpdateAsync(TestTableModel model)
    {
        var entity = model.ToEntity();
        
        var updated = await base.UpdateAsync(entity);
        
        return updated.ToModel();
    }

    public new async Task<TestTableModel?> GetByIdAsync(int id)
    {
        var entity = await _dbContext.TestTable.FindAsync(id);
        return entity?.ToModel();
    }
    #endregion
}