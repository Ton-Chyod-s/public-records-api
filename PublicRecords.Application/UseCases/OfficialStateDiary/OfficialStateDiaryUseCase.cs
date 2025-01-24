﻿using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.OfficialStateDiary;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.Services.OfficialElectronicDiary;
using PublicRecords.Domain.Interface.UseCases.OfficialElectronicDiary;
using OneOf;

namespace PublicRecords.Application.UseCases.OfficialElectronicDiary
{
    internal class OfficialStateDiaryUseCase
        (
            IOfficialStateDiaryService officialElectronicDiaryService
        ) : IOfficialStateDiaryUseCase
    {
        private readonly IOfficialStateDiaryService _officialElectronicDiaryService = officialElectronicDiaryService;

        public async Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>> GetOfficialStateDiary(string name, string year)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
                return new InvalidName();

            var yearValid = year.EnsureValidYear();

            if (yearValid.IsError())
                return yearValid.GetError();

            return await _officialElectronicDiaryService.GetOfficialStateDiaryResponse(name, year);
        }
            
    }
}
