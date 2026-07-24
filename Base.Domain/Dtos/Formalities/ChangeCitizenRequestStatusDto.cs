using Base.Domain.Emuns;

namespace Base.Domain.Dtos.Formalities;

public class ChangeCitizenRequestStatusDto
{
    public RequestStatus Status { get; set; }
}