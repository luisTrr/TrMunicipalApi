namespace Base.Domain.Models.Formalities;

public class RequestTypeModel : TraceModel
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime LastModifiedByAt { get; private set; }
    public int LastModifiedBy { get; private set; }


    // Crear un nuevo tipo de trámite
    public RequestTypeModel(
        string name,
        string description)
    {
        ValidateName(name);
        ValidateDescription(description);

        Name = name;
        Description = description;
        IsActive = true;
    }


    // Reconstruir desde la base de datos
    public RequestTypeModel(
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
                "El ID del tipo de trámite es inválido."));

        ValidateName(name);
        ValidateDescription(description);

        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;

        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModifiedBy;
    }


    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            AddError(new Exception(
                "El nombre del tipo de trámite es obligatorio."));
        }

        if (name?.Length > 150)
        {
            AddError(new Exception(
                "El nombre no puede superar los 150 caracteres."));
        }
    }


    private void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            AddError(new Exception(
                "La descripción del tipo de trámite es obligatoria."));
        }

        if (description?.Length > 500)
        {
            AddError(new Exception(
                "La descripción no puede superar los 500 caracteres."));
        }
    }
}