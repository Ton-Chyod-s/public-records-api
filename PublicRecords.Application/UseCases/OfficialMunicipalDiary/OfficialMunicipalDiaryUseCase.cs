using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.OfficialStateDiary;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.Services.OfficialStateDiary;
using PublicRecords.Domain.Interface.UseCases.OfficialStateDiary;
using OneOf;

namespace PublicRecords.Application.UseCases.OfficialStateDiary
{
    internal class OfficialMunicipalDiaryUseCase
        (
            IOfficialMunicipalDiaryService officialStateDiaryService
        ) : IOfficialMunicipalDiaryUseCase
    {
        private readonly IOfficialMunicipalDiaryService _officialStateDiaryService = officialStateDiaryService;

        public async Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialMunicipalDiary(string name, string year)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                return new InvalidName();

            var yearValid = year.EnsureValidYear();

            if (yearValid.IsError())
                return yearValid.GetError();

            return await _officialStateDiaryService.GetOfficialMunicipalDiaryResponse(name, yearValid.GetValue());
        }
    }
}

