using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Infrastructure.Database.EntityFramework.Entity;

public abstract class BaseEntity
{
    [Column("createdAt")]
    public DateTime CreatedAt { get; set; }
    [Column("createdBy")]
    public int CreatedBy { get; set; }
    [Column("lastModifiedByAt")]
    public DateTime LastModifiedByAt { get; set; }
    [Column("lastModifiedBy")]
    public int LastModifiedBy { get; set; } 
}