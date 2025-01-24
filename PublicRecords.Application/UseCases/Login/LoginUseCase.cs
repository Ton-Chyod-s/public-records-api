using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Common;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Interface.Services.Token;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Login;
using OneOf;

namespace PublicRecords.Application.UseCases.Login
{
    internal class LoginUseCase
    (
        IUnitOfWork unitOfWork,
        ITokenService tokenService
    ) : ILoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<OneOf<ResponseTokenDTO, BaseError>> LoginWithApp(ResquestAddOrLoginDTO resquestAddOrUpdateLoginDTO)
        {
            var userResult = await _unitOfWork.UserRepository.GetUserByName(resquestAddOrUpdateLoginDTO.UserName);

            if (userResult is null)
                return new UserNotFound();

            var token = _tokenService.GenerateToken(userResult);

            var desaralizeToken = DesaralizeToken(token);

            if (token is null)
                return new UnauthorizedAccess();

            //var TokenResult = await _unitOfWork.UserRepository.AddOrUpdateToken(desaralizeToken, userResult.Id);    

            //if (TokenResult.IsError())
            //    return TokenResult.GetError();

            return token;
        }

        internal string DesaralizeToken(ResponseTokenDTO responseTokenDTO)
        {
            return responseTokenDTO.Bearer;
        }

    }
}
