using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Login;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace PublicRecords.Application.UseCases.Login
{
    internal class DeleteLoginUseCase
        (
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : IDeleteLoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<OneOf<bool, BaseError>> DeleteUser()
        {
            var userName = _httpContextAccessor.HttpContext.GetSystemIdentifierIdByContext();

            var User = await _unitOfWork.UserRepository.GetUserByName(userName);

            if (User is null)
                return new UserNotFound();

            var delete = await _unitOfWork.UserRepository.DeleteUser(User.Id);

            return true;
        }
    }
}
