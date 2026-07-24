namespace Base.Domain.Models.Authentication;

public class PermissionModel : TraceModel
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }


    public PermissionModel(
        string name,
        string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            AddError(new Exception(
                "El nombre del permiso es obligatorio."));

        if (name?.Length > 100)
            AddError(new Exception(
                "El nombre del permiso no puede superar los 100 caracteres."));

        if (string.IsNullOrWhiteSpace(description))
            AddError(new Exception(
                "La descripción del permiso es obligatoria."));

        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}