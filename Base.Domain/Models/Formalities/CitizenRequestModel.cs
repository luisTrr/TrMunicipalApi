using Base.Domain.Emuns;

namespace Base.Domain.Models.Formalities;

public class CitizenRequestModel : TraceModel
{
    public int Id { get; private set; }

    public string CitizenName { get; private set; }

    public int RequestTypeId { get; private set; }

    public string Description { get; private set; }

    public DateTime RegisteredAt { get; private set; }

    public RequestStatus Status { get; private set; }

    public RequestPriority Priority { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime LastModifiedByAt { get; private set; }
    public int LastModifiedBy { get; private set; }


    // Crear una solicitud
    public CitizenRequestModel(
        string citizenName,
        int requestTypeId,
        string description,
        RequestPriority priority)
    {
        ValidateCitizenName(citizenName);
        ValidateRequestType(requestTypeId);
        ValidateDescription(description);
        ValidatePriority(priority);

        CitizenName = citizenName;
        RequestTypeId = requestTypeId;
        Description = description;
        RegisteredAt = DateTime.UtcNow;
        Status = RequestStatus.Registered;
        Priority = priority;
        IsDeleted = false;
    }
    
    public void Update(
        string citizenName,
        int requestTypeId,
        string description,
        RequestPriority priority)
    {
        ValidateCitizenName(citizenName);
        ValidateRequestTypeId(requestTypeId);
        ValidateDescription(description);

        if (HasErrors())
            return;

        CitizenName = citizenName;
        RequestTypeId = requestTypeId;
        Description = description;
        Priority = priority;
    }


    // Reconstruir desde la base de datos
    public CitizenRequestModel(
        int id,
        string citizenName,
        int requestTypeId,
        string description,
        DateTime registeredAt,
        RequestStatus status,
        RequestPriority priority,
        bool isDeleted,
        DateTime createdAt,
        int createdBy,
        DateTime lastModifiedByAt,
        int lastModifiedBy)
    {
        if (id <= 0)
            AddError(new Exception(
                "El ID de la solicitud es inválido."));

        ValidateCitizenName(citizenName);
        ValidateRequestType(requestTypeId);
        ValidateDescription(description);
        ValidatePriority(priority);

        Id = id;
        CitizenName = citizenName;
        RequestTypeId = requestTypeId;
        Description = description;
        RegisteredAt = registeredAt;
        Status = status;
        Priority = priority;
        IsDeleted = isDeleted;

        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastModifiedByAt = lastModifiedByAt;
        LastModifiedBy = lastModifiedBy;
    }


    private void ValidateCitizenName(string citizenName)
    {
        if (string.IsNullOrWhiteSpace(citizenName))
        {
            AddError(new Exception(
                "El nombre del ciudadano es obligatorio."));
        }

        if (citizenName?.Length > 150)
        {
            AddError(new Exception(
                "El nombre del ciudadano no puede superar los 150 caracteres."));
        }
    }


    private void ValidateRequestType(int requestTypeId)
    {
        if (requestTypeId <= 0)
        {
            AddError(new Exception(
                "El tipo de trámite es obligatorio."));
        }
    }


    private void ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            AddError(new Exception(
                "La descripción es obligatoria."));
        }

        if (description?.Length > 2000)
        {
            AddError(new Exception(
                "La descripción no puede superar los 2000 caracteres."));
        }
    }


    private void ValidatePriority(RequestPriority priority)
    {
        if (!Enum.IsDefined(priority))
        {
            AddError(new Exception(
                "La prioridad seleccionada no es válida."));
        }
    }
    
    private void ValidateRequestTypeId(int requestTypeId)
    {
        if (requestTypeId <= 0)
        {
            AddError(
                new Exception(
                    "El tipo de trámite no es válido."));
        }
    }
    public void ChangeStatus(RequestStatus newStatus)
    {
        if (Status == RequestStatus.Resolved ||
            Status == RequestStatus.Rejected)
        {
            AddError(
                new Exception(
                    "No se puede cambiar el estado de una solicitud finalizada."));
        
            return;
        }

        var validTransition =
            Status switch
            {
                RequestStatus.Registered =>
                    newStatus == RequestStatus.UnderReview,

                RequestStatus.UnderReview =>
                    newStatus == RequestStatus.InProgress ||
                    newStatus == RequestStatus.Rejected,

                RequestStatus.InProgress =>
                    newStatus == RequestStatus.Resolved,

                _ => false
            };

        if (!validTransition)
        {
            AddError(
                new Exception(
                    $"No es válida la transición de {Status} a {newStatus}."));

            return;
        }

        Status = newStatus;
    }
}