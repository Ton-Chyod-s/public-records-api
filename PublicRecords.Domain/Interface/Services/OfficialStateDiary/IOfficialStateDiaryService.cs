using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.Services.OfficialElectronicDiary
{
    public interface IOfficialStateDiaryService
    {
        Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialStateDiaryResponse(string name, string year);
    }
}
