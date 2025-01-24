using PublicRecords.Domain.DTOs.Person;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Person
{
    public interface IPersonUseCase
    {
        Task<OneOf<bool, BaseError>> ExecuteAddOrUpdatePerson(PersonDTO personEnum);
    }
}
