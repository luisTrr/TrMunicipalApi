namespace Base.Infrastructure.Database.EntityFramework.Entity.Authentication;

public class RoleEntity : BaseEntity, IIdentifiable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
}