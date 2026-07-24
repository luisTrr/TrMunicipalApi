using Base.Domain.Models.Authentication;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;

public static class UserRoleExtension
{
    public static UserRoleEntity ToEntity(
        this UserRoleModel model)
    {
        return new UserRoleEntity
        {
            Id = model.Id,
            UserId = model.UserId,
            RoleId = model.RoleId,

            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            LastModifiedByAt = model.LastModifiedByAt,
            LastModifiedBy = model.LastModifiedBy
        };
    }


    public static UserRoleModel ToModel(
        this UserRoleEntity entity)
    {
        return new UserRoleModel(
            entity.Id,
            entity.UserId,
            entity.RoleId,
            entity.CreatedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy
        );
    }
}