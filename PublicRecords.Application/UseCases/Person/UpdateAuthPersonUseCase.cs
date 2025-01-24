using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Person;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace PublicRecords.Application.UseCases.Person
{
    internal class UpdateAuthPersonUseCase
        (
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : IUpdateAuthPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<OneOf<bool, BaseError>> UpdateAuthPerson()
        {
            var userName = _httpContextAccessor.HttpContext.GetSystemIdentifierIdByContext();

            var userResult = await _unitOfWork.UserRepository.GetUserByName(userName);

            if (userResult is null)
                return new UserNotFound();

            return await _unitOfWork.PersonRepository.UpdateAuthorized(userResult.Id);
        }
    }
}
