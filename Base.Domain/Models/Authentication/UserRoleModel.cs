namespace Base.Domain.Models.Authentication;

public class UserRoleModel : TraceModel
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public int RoleId { get; private set; }

    public DateTime AssignedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime LastModifiedByAt { get; private set; }
    public int LastModifiedBy { get; private set; }


    public UserRoleModel(
        int id,
        int userId,
        int roleId,
        DateTime createdAt,
        int createdBy,
        DateTime lastModifiedByAt,
        int lastModified)
    {
        if (id <= 0)
            AddError(new Exception(
                "El ID de la relación es inválido."));

        if (userId <= 0)
            AddError(new Exception(
                "El ID del usuario es inválido."));

        if (roleId <= 0)
            AddError(new Exception(
                "El ID del rol es inválido."));

        Id = id;
        UserId = userId;
        RoleId = roleId;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModified;
    }
}