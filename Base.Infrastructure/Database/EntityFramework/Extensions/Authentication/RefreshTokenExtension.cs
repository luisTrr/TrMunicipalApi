using Base.Domain.Models.Authentication;
using Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

namespace Base.Infrastructure.Database.EntityFramework.Extensions.Authentication;

public static class RefreshTokenExtension
{
    public static RefreshTokenEntity ToEntity(
        this RefreshTokenModel model)
    {
        return new RefreshTokenEntity
        {
            Id = model.Id,
            UserId = model.UserId,
            Token = model.Token,
            ExpiresAt = model.ExpiresAt,
            RevokedAt = model.RevokedAt,

            CreatedAt = model.CreatedAt,
            CreatedBy = model.CreatedBy,
            LastModifiedByAt = model.LastModifiedByAt,
            LastModifiedBy = model.LastModifiedBy
        };
    }


    public static RefreshTokenModel ToModel(
        this RefreshTokenEntity entity)
    {
        return new RefreshTokenModel(
            entity.Id,
            entity.UserId,
            entity.Token,
            entity.ExpiresAt,
            entity.CreatedAt,
            entity.RevokedAt,
            entity.CreatedBy,
            entity.LastModifiedByAt,
            entity.LastModifiedBy
        );
    }
}