using Base.Domain.Models;
using Base.Infrastructure.Database.EntityFramework.Entity;

namespace Base.Infrastructure.Database.EntityFramework.Extensions;

public static class TestTableExtension
{
    public static TestTableEntity ToEntity(this TestTableModel model)
    {
        return new TestTableEntity
        {
            Id = model.Id,
            TimesTamp = model.TimesTamp,
            InvoiceLinkExtern = model.InvoiceLinkExtern,
            InvoiceRollExtern = model.InvoiceRollExtern,
            InvoiceNumberExtern = model.InvoiceNumberExtern,
            IziIdExtern = model.IziIdExtern
        };
    }

    public static TestTableModel ToModel(this TestTableEntity entity)
    {
        return new TestTableModel(
            entity.Id,
            entity.TimesTamp,
            entity.InvoiceLinkExtern,
            entity.InvoiceRollExtern,
            entity.InvoiceNumberExtern,
            entity.IziIdExtern
        );
    }
}