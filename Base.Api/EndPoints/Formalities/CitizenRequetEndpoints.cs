using Base.Api.EndPoints.Common;
using Base.Aplication.Services.Formalities;
using Base.Domain.Dtos.Formalities;

namespace Base.Api.EndPoints.Formalities;

public static class CitizenRequestEndpoints
{
    internal static void MapCitizenRequestEndpoints(
        this WebApplication webApp)
    {
        webApp
            .MapGroup("citizen-requests")
            .WithTags("Citizen Requests")
            .MapGroupEndpoints();
    }


    private static void MapGroupEndpoints(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(
            "/",
            (CreateCitizenRequestDto dto, CitizenRequestService service) =>
                    service.CreateAsync(dto).ToApiResult());


        builder.MapGet(
            "/",
            ([AsParameters] PaginationRequestDto pagination, CitizenRequestService service) =>
                service.GetPagedAsync(pagination).ToApiResult());


        builder.MapGet(
            "/{id:int}",
            (int id, CitizenRequestService service) =>
                    service.GetByIdAsync(id).ToApiResult());


        builder.MapPut(
            "/{id:int}",
            (int id, UpdateCitizenRequestDto dto, CitizenRequestService service) =>
                    service.UpdateAsync(id, dto).ToApiResult());


        builder.MapPatch(
            "/{id:int}/status",
            (int id, ChangeCitizenRequestStatusDto dto, CitizenRequestService service) =>
                    service.ChangeStatusAsync(id, dto).ToApiResult());


        builder.MapDelete(
            "/{id:int}",
            (int id, CitizenRequestService service) =>
                    service.DeleteAsync(id).ToApiResult());
    }
}