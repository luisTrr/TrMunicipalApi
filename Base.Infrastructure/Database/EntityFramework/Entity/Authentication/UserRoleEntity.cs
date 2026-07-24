namespace Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

public class UserRoleEntity : BaseEntity, IIdentifiable
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual UserEntity User { get; set; }

    public virtual RoleEntity Role { get; set; }
}