using Base.Domain.Emuns;

namespace Base.Domain.Dtos.Formalities;

public class CreateCitizenRequestDto
{
    public string CitizenName { get; set; }

    public int ProcedureTypeId { get; set; }

    public string Description { get; set; }

    public RequestPriority Priority { get; set; }
}