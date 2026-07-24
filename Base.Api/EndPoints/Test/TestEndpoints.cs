using Base.Api.EndPoints.Common;
using Base.Aplication.Services;
using Base.Domain.Dtos.Test;
using Base.Domain.Models;

namespace Base.Api.EndPoints.Test;

public static class TestEndpoints
{
    internal static void MapTestEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("test-endpoints")
            .WithTags("TEST")
            .MapGroupEndpoint();
    }

    private static void MapGroupEndpoint(this RouteGroupBuilder builder)
    {
        builder.MapPost(
            "/",
            (TestTableModel model, TestTableService service) => 
                service.Save(model).ToApiResult());

        builder.MapPut(
            "/{id:int}",
            (int id, UpdateTestTableDto dto, TestTableService service) =>
                service.Update(id, dto).ToApiResult());

        builder.MapGet(
            "/{id:int}",
            (int id, TestTableService service) =>
                service.GetByIdAsync(id).ToApiResult());

        builder.MapDelete(
            "/{id:int}",
            (int id, TestTableService service) =>
                service.DeleteAsync(id).ToApiResult());
    }
}