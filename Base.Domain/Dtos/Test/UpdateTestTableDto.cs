namespace Base.Domain.Dtos.Test;

public record UpdateTestTableDto(
  DateTime? TimesTamp,
  string? InvoiceLinkExtern,
  string? InvoiceRollExtern,
  int? InvoiceNumberExtern,
  int? IziIdExtern
);