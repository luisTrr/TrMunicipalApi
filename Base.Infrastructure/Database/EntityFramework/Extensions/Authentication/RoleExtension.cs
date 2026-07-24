using Base.Domain.Models.Authentication;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;

public static class RoleExtension
{
    public static RoleEntity ToEntity(
        this RoleModel model)
    {
        return new RoleEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            IsActive = model.IsActive,

            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            LastModifiedByAt = model.LastModifiedByAt,
            LastModifiedBy = model.LastModifiedBy
        };
    }


    public static RoleModel ToModel(
        this RoleEntity entity)
    {
        return new RoleModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.IsActive,
            entity.CreatedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy
        );
    }
}