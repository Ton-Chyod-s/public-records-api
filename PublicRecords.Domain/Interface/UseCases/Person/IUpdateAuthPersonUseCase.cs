using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Person
{
    public interface IUpdateAuthPersonUseCase
    {
        Task<OneOf<bool, BaseError>> UpdateAuthPerson();
    }
}
