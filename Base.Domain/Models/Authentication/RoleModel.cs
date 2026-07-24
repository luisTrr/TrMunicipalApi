namespace Base.Domain.Models.Authentication;

public class RoleModel : TraceModel
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    
    public DateTime LastModifiedByAt { get; private set; }
    
    public int LastModifiedBy { get; private set; }


    public RoleModel(
        int id,
        string name,
        string description,
        bool isActive,
        DateTime createdAt,
        int createdBy,
        DateTime lastModifiedByAt,
        int lastModifiedBy)
    {
        if (id <= 0)
            AddError(new Exception(
                "El ID del rol es inválido."));

        if (string.IsNullOrWhiteSpace(name))
            AddError(new Exception(
                "El nombre del rol es obligatorio."));

        if (string.IsNullOrWhiteSpace(description))
            AddError(new Exception(
                "La descripción del rol es obligatoria."));

        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModifiedBy;
    }
}