using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.OfficialElectronicDiary
{
    public interface IOfficialStateDiaryUseCase
    {
        Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialStateDiary(string name, string year);
    }
}
