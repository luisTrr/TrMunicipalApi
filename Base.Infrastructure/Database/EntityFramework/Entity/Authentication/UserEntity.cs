namespace Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

public class UserEntity : BaseEntity, IIdentifiable
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

    public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
}