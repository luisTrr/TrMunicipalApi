using Base.Domain.Models.Authentication;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;

public static class UserExtension
{
    public static UserEntity ToEntity(
        this UserModel model)
    {
        return new UserEntity
        {
            Id = model.Id,
            Username = model.Username,
            Email = model.Email,
            PasswordHash = model.PasswordHash,
            IsActive = model.IsActive,

            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            LastModifiedByAt = model.LastModifiedByAt,
            LastModifiedBy = model.LastModifiedBy
        };
    }


    public static UserModel ToModel(
        this UserEntity entity)
    {
        return new UserModel(
            entity.Id,
            entity.Username,
            entity.Email,
            entity.PasswordHash,
            entity.IsActive,
            entity.CreatedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy
        );
    }
}