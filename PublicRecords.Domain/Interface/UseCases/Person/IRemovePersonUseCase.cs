using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Person
{
    public interface IRemovePersonUseCase
    {
        Task<OneOf<bool, BaseError>> RemovePerson(long id);
    }
}
