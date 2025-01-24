using PublicRecords.Domain.DTOs.Person;
using PublicRecords.Domain.Entities.Person;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.Repository
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        //Task<OneOf<bool, BaseError>> AddOrUpdatePerson(string name, string email, long userId);
        Task<OneOf<bool?, BaseError>> RemovePerson(long personId);
        Task<ResponsePersonDTO> GetPersonDTOAsync(string name);
        Task<OneOf<long, BaseError>> AddSession(long personId, string year);
        Task<OneOf<bool, BaseError>> addOfficialDiary(List<Dictionary<string, string>> responseOfficialMunicipalDiaryDTO);
        Task<bool> UpdateAuthorized(long userId);
        Task<OneOf<bool, BaseError>> AddPerson(string name, string email, long userId);
        Task<OneOf<bool, BaseError>> UpdatePerson(string name, string email, long userId);
        Task<OneOf<Person?, BaseError>> GetPersonById(long userId);
    }
}
