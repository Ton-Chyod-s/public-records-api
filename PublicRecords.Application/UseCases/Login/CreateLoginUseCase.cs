using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Login;
using OneOf;

namespace PublicRecords.Application.UseCases.Login
{
    internal class CreateLoginUseCase
        (
            IUnitOfWork unitOfWork
        ) : ICreateLoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OneOf<bool, BaseError>> CreateLogin(ResquestAddOrLoginDTO content)
        {
            if (content.UserName is null || content is null)
                return new UserNotSaved();

            var getUser = await _unitOfWork.UserRepository.GetUserByName(content.UserName);

            if (getUser is not null)
                return new UserNotSaved();

            return await _unitOfWork.UserRepository.AddUser(content);

        }

    }
}
