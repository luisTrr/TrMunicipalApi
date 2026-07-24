namespace Base.Infrastructure.Database.EntityFramework.Entity.Formalities;

public class RequestTypeEntity : BaseEntity, IIdentifiable
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CitizenRequestEntity>
        CitizenRequests { get; set; }
        = new List<CitizenRequestEntity>();
}