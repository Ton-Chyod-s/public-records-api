using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.Services.OfficialStateDiary
{
    public interface IOfficialMunicipalDiaryService
    {
        Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialMunicipalDiaryResponse(string name, string year);
    }
}
