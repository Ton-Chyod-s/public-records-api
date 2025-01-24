using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Person;
using OneOf;

namespace PublicRecords.Application.UseCases.Person
{
    internal class RemovePersonUseCase
        (
            IUnitOfWork unitOfWork
        ) : IRemovePersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OneOf<bool, BaseError>> RemovePerson(long id)
        {
            var removePerson = await _unitOfWork.PersonRepository.RemovePerson(id);

            if (removePerson.IsError())
                return removePerson.GetError();

            return true;
        }

    }
}
