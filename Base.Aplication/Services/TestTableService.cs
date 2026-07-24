using System.Net;
using Base.Domain.Dtos.Test;
using Base.Domain.Models;
using Base.Domain.Repositories;
using Base.Domain.Responses;

namespace Base.Aplication.Services;

public class TestTableService(ITestTableRepository repository)
{
    public async Task<Result<TestTableModel>> Save(TestTableModel model)
    {
        if (model.Id == 0)
        {
            var created = await repository.CreateAsync(model);
            return Result<TestTableModel>.Success(created, HttpStatusCode.OK);
        }
        return Result<TestTableModel>.Failure(new List<string> { "Falla al crear el recurso" }, HttpStatusCode.NotFound);
    }

    public async Task<Result<TestTableModel>> Update(int id, UpdateTestTableDto update)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            return Result<TestTableModel>.Failure(new List<string>(){$"El recurso con Id {id} no fue encontrado."}, HttpStatusCode.NotFound);
        }
        var test = await repository.GetByIdAsync(id);

        if (test == null)
            return null;
        
        PatchHelper.ApplyPath(test, update);

        var result = await repository.UpdateAsync(test);

        return Result<TestTableModel>.Success(result, HttpStatusCode.OK);
    }
    
    // public async Task<Result<List<IziModel>>> GetAllAsync()
    // {
    //     var list = await repository.GetAllAsync();
    //     return Result<List<IziModel>>.Success(list, HttpStatusCode.OK);
    // }
    //
    public async Task<Result<TestTableModel>> GetByIdAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);
        if (result == null)
            return Result<TestTableModel>.Failure(new List<string> { "Recurso no encontrado" }, HttpStatusCode.NotFound);
        return Result<TestTableModel>.Success(result, HttpStatusCode.OK);
    }
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
            return Result<bool>.Failure(new List<string> { "El recurso no existe" }, HttpStatusCode.NotFound);
        var deleted = await repository.DeleteHardAsync(id);
        if (!deleted)
            return Result<bool>.Failure(new List<string> { "No se pudo eliminar el recurso" },
                HttpStatusCode.BadRequest);
        return Result<bool>.Success(true, HttpStatusCode.OK);
    }
}