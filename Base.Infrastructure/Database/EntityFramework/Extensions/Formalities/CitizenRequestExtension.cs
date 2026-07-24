using Base.Domain.Models.Formalities;
using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Formalities;

public static class CitizenRequestExtension
{
    public static CitizenRequestEntity ToEntity(
        this CitizenRequestModel model)
    {
        return new CitizenRequestEntity
        {
            Id = model.Id,
            CitizenName = model.CitizenName,
            RequestTypeId = model.RequestTypeId,
            Description = model.Description,
            RegisteredAt = model.RegisteredAt,
            Status = model.Status,
            Priority = model.Priority,
            IsDeleted = model.IsDeleted,
            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            LastModifiedByAt = model.LastModifiedByAt,
            LastModifiedBy = model.LastModifiedBy
        };
    }


    public static CitizenRequestModel ToModel(
        this CitizenRequestEntity entity)
    {
        return new CitizenRequestModel(
            entity.Id,
            entity.CitizenName,
            entity.RequestTypeId,
            entity.Description,
            entity.RegisteredAt,
            entity.Status,
            entity.Priority,
            entity.IsDeleted,
            entity.CreatedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy);
    }
}