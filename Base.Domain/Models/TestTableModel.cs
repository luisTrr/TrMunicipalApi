namespace Base.Domain.Models;

public class TestTableModel : TraceModel
{
    public int Id { get; private set; }
    public DateTime TimesTamp { get; private set; }
    public string InvoiceLinkExtern { get; private set; }
    public string InvoiceRollExtern { get; private set; }
    public int InvoiceNumberExtern { get; private set; }
    public int IziIdExtern { get; private set; }

    public TestTableModel(int id, DateTime timesTamp, string invoiceLinkExtern, string invoiceRollExtern, int invoiceNumberExtern, int iziIdExtern)
    {
        if (id < 0)
            AddError(new Exception("El ID es invalido "));
        if (iziIdExtern < 0)
            AddError(new Exception("El ID es invalido "));
        if (invoiceNumberExtern < 0)
            AddError(new Exception("Numero de factura invalido "));
        
        Id= id;
        TimesTamp = timesTamp;
        InvoiceLinkExtern = invoiceLinkExtern;
        InvoiceRollExtern = invoiceRollExtern;
        InvoiceNumberExtern = invoiceNumberExtern;
        IziIdExtern =  iziIdExtern;
    }
}