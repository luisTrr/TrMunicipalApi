using System.Net;
using Base.Domain.Dtos.Formalities;
using Base.Domain.Emuns;
using Base.Domain.Models.Formalities;
using Base.Domain.Repositories.Formalities;
using Base.Domain.Responses;

namespace Base.Aplication.Services.Formalities;

public class CitizenRequestService(ICitizenRequestRepository repository)
{
    public async Task<Result<CitizenRequestModel>> CreateAsync(CreateCitizenRequestDto dto)
    {
        if (!Enum.IsDefined(typeof(RequestPriority), dto.Priority))
        {
            return Result<CitizenRequestModel>.Failure(new List<string>
                {
                    "La prioridad seleccionada no es válida."
                }, HttpStatusCode.BadRequest);
        }

        var priority = (RequestPriority)dto.Priority;

        var model =
            new CitizenRequestModel(
                dto.CitizenName,
                dto.ProcedureTypeId,
                dto.Description,
                priority);

        if (model.HasErrors())
        {
            return Result<CitizenRequestModel>.Failure(
                model.GetAllMessageErrors(),
                HttpStatusCode.BadRequest);
        }

        var created =
            await repository.CreateAsync(model);

        return Result<CitizenRequestModel>.Success(
            created,
            HttpStatusCode.Created);
    }


    public async Task<Result<PagedResult<CitizenRequestModel>>> GetPagedAsync(PaginationRequestDto pagination)
    {
        var result = await repository.GetPagedAsync(pagination.Page, pagination.PageSize);

        return Result<PagedResult<CitizenRequestModel>>
            .Success(result, HttpStatusCode.OK);
    }


    public async Task<Result<CitizenRequestModel>> GetByIdAsync(int id)
    {
        var result = await repository.GetByIdAsync(id);

        if (result == null)
        {
            return Result<CitizenRequestModel>.Failure(new List<string>
                {
                    "La solicitud no fue encontrada."
                }, HttpStatusCode.NotFound);
        }

        return Result<CitizenRequestModel>.Success(result, HttpStatusCode.OK);
    }


    public async Task<Result<CitizenRequestModel>> UpdateAsync(int id, UpdateCitizenRequestDto dto)
    {
        var model = await repository.GetByIdAsync(id);

        if (model == null)
        {
            return Result<CitizenRequestModel>.Failure(new List<string>
                {
                    "La solicitud no fue encontrada."
                }, HttpStatusCode.NotFound);
        }

        model.Update(
            dto.CitizenName,
            dto.ProcedureTypeId,
            dto.Description,
            dto.Priority);

        if (model.HasErrors())
        {
            return Result<CitizenRequestModel>.Failure(
                model.GetAllMessageErrors(),
                HttpStatusCode.BadRequest);
        }

        var updated =
            await repository.UpdateAsync(model);

        return Result<CitizenRequestModel>.Success(
            updated,
            HttpStatusCode.OK);
    }


    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var exists = await repository.ExistsByIdAsync(id);

        if (!exists)
        {
            return Result<bool>.Failure(new List<string>
                {
                    "La solicitud no existe."
                }, HttpStatusCode.NotFound);
        }

        var deleted = await repository.DeleteHardAsync(id);

        if (!deleted)
        {
            return Result<bool>.Failure(new List<string>
                {
                    "No se pudo eliminar la solicitud."
                }, HttpStatusCode.BadRequest);
        }

        return Result<bool>.Success(true, HttpStatusCode.OK);
    }
    
    public async Task<Result<CitizenRequestModel>> ChangeStatusAsync(int id, ChangeCitizenRequestStatusDto dto)
    {
        var model =
            await repository.GetByIdAsync(id);

        if (model == null)
        {
            return Result<CitizenRequestModel>.Failure(new List<string>
                {
                    "La solicitud no fue encontrada."
                }, HttpStatusCode.NotFound);
        }

        model.ChangeStatus(dto.Status);

        if (model.HasErrors())
        {
            return Result<CitizenRequestModel>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);
        }

        var updated = await repository.UpdateAsync(model);

        return Result<CitizenRequestModel>.Success(updated, HttpStatusCode.OK);
    }
}