namespace Base.Domain.Models.Authentication;

public class RolePermissionModel : TraceModel
{
    public int Id { get; private set; }

    public int RoleId { get; private set; }

    public int PermissionId { get; private set; }

    public DateTime AssignedAt { get; private set; }


    public RolePermissionModel(
        int roleId,
        int permissionId)
    {
        if (roleId <= 0)
            AddError(new Exception(
                "El ID del rol es inválido."));

        if (permissionId <= 0)
            AddError(new Exception(
                "El ID del permiso es inválido."));

        RoleId = roleId;
        PermissionId = permissionId;
        AssignedAt = DateTime.UtcNow;
    }
}