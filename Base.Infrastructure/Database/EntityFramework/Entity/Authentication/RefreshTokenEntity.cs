namespace Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

public class RefreshTokenEntity : BaseEntity, IIdentifiable
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public virtual UserEntity User { get; set; }
}