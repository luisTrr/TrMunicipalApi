using Base.Domain.Emuns;

namespace Base.Infrastructure.Database.EntityFramework.Entity.Formalities;

public class CitizenRequestEntity
    : BaseEntity,
        IIdentifiable
{
    public int Id { get; set; }

    public string CitizenName { get; set; }

    public int RequestTypeId { get; set; }

    public string Description { get; set; }

    public DateTime RegisteredAt { get; set; }

    public RequestStatus Status { get; set; }

    public RequestPriority Priority { get; set; }

    public bool IsDeleted { get; set; }

    public virtual RequestTypeEntity RequestType { get; set; }
}