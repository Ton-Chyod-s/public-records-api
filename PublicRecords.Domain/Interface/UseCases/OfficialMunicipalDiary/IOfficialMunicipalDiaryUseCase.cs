using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.OfficialStateDiary
{
    public interface IOfficialMunicipalDiaryUseCase
    {
        Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialMunicipalDiary(string name, string year);
    }
}
