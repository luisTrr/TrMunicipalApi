using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Infrastructure.Database.EntityFramework.Entity;
[Table("TestTable", Schema = "TST")]
public class TestTableEntity : BaseEntity, IIdentifiable
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
    public int Id { get; set; }
    [Required, Column("timesTamp")]
    public DateTime TimesTamp { get; set; }
    [Required, Column("invoiceLinkExtern"), MaxLength(80)]
    public string InvoiceLinkExtern { get; set; }
    [Required, Column("invoiceRollExtern"), MaxLength(80)]
    public string InvoiceRollExtern  { get; set; }
    [Column("invoiceNumberExtern")]
    public int InvoiceNumberExtern { get; set; }
    [Column("iziIdExtern")]
    public int IziIdExtern { get; set; }
}