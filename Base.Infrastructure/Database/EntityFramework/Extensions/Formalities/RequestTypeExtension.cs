using Base.Domain.Models.Formalities;
using Base.Infrastructure.Database.EntityFramework.Entity.Formalities;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Formalities;

public static class RequestTypeExtension
{
    public static RequestTypeEntity ToEntity(
        this RequestTypeModel model)
    {
        return new RequestTypeEntity
        {
            Id = model.Id,

            Name = model.Name,

            Description = model.Description,

            IsActive = model.IsActive,

            CreatedAt = model.CreatedAt,

            CreatedBy = model.CreatedBy,

            LastModifiedByAt =
                model.LastModifiedByAt,

            LastModifiedBy =
                model.LastModifiedBy
        };
    }


    public static RequestTypeModel ToModel(
        this RequestTypeEntity entity)
    {
        return new RequestTypeModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.IsActive,
            entity.CreatedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy);
    }
}