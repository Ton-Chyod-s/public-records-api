using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.OfficialStateDiary;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Domain.Interface.Services.OfficialElectronicDiary;
using PublicRecords.Domain.Interface.Services.OfficialStateDiary;
using PublicRecords.Domain.Interface.UseCases.SaveAndNotify;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace PublicRecords.Application.UseCases.SaveAndNotify
{
    internal class SaveAndNotifyUseCase 
        (
            IOfficialMunicipalDiaryService officialStateDiaryService,
            IOfficialStateDiaryService officialElectronicDiaryService,
            IHttpContextAccessor httpContextAccessor,
            IPersonRepository personRepository

        ) : ISaveAndNotifyUseCase
    {
        private readonly IOfficialMunicipalDiaryService _officialStateDiaryService = officialStateDiaryService;
        private readonly IOfficialStateDiaryService _officialElectronicDiaryService = officialElectronicDiaryService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<OneOf<bool, BaseError>> SaveAndNotify()
        {
            var userName = _httpContextAccessor.HttpContext.GetSystemIdentifierIdByContext();

            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 3)
                return new InvalidName();

            var intYear = DateTime.Now.Year - 2;    

            var stringYear = intYear.ToString();

            var yearValid = stringYear.EnsureValidYear();

            if (yearValid.IsError())
                return yearValid.GetError();

            var personData = await _personRepository.GetPersonDTOAsync(userName);

            var sessionData = await _personRepository.AddSession(personData.Id, yearValid.GetValue());

            if (sessionData.IsError())
                return sessionData.GetError();

            var fetchAndProcessDiaries = await FetchAndProcessDiaries(personData.Name, stringYear, personData.Id, sessionData.GetValue());



            // TODO: Enviar email

            return true;
        }


        internal async Task<OneOf<bool, BaseError>> FetchAndProcessDiaries(string name, string year, long personId, long sessionId)
        {
            var municipalDiaryResult = await FetchAndProcessDiaryData(() => _officialStateDiaryService.GetOfficialMunicipalDiaryResponse(name, year), personId, sessionId);

            if (municipalDiaryResult.IsError())
                return municipalDiaryResult.GetError();

            var electronicDiaryResult = await FetchAndProcessDiaryData(() => _officialElectronicDiaryService.GetOfficialStateDiaryResponse(name, year), personId, sessionId);

            if (electronicDiaryResult.IsError()) 
                return electronicDiaryResult.GetError();

            return true;
        }

        internal async Task<OneOf<List<Dictionary<string, string>>, BaseError>> FetchAndProcessDiaryData(Func<Task<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>> fetchDiaryData, long personId, long sessionId) 
        {
            var diaryData = await fetchDiaryData();

            if (diaryData.IsError())
                    return diaryData.GetError();

            var newDiaries = diaryData.GetValue();

            var newList = newDiaries.Select(item => new Dictionary<string, string>
            {
                { "Number", item.Number },
                { "Day", item.Day },
                { "File", item.File },
                { "Description", item.Description },
                { "SessionId", sessionId.ToString() },
                { "PersonId", personId.ToString() },
                { "Type", item.Type.ToString() }
            }).ToList();

            var addOfficialDiary = await _personRepository.addOfficialDiary(newList);

            if (addOfficialDiary.IsError())
                return addOfficialDiary.GetError(); 

            return newList;

        }

    }
}
