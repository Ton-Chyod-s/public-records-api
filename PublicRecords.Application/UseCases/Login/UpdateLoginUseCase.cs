using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Enums.User;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Common;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.Services.Token;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Login;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace PublicRecords.Application.UseCases.Login
{
    internal class UpdateLoginUseCase
        (
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ITokenService tokenService

        ) : IUpdateLoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<OneOf<ResponseTokenDTO, BaseError>> UpdateLogin(RequestUpdateLoginDTO requestUpdateLoginDTO)
        {
            var userName = _httpContextAccessor.HttpContext.GetSystemIdentifierIdByContext();

            var userType = requestUpdateLoginDTO.Type ?? UserEnum.User;
            var update = await _unitOfWork.UserRepository.UpdateUser(userName, requestUpdateLoginDTO);

            var userResult = await _unitOfWork.UserRepository.GetUserByName(requestUpdateLoginDTO.Name);

            if (userResult is null)
                return new UserNotFound();

            var token = _tokenService.GenerateToken(userResult);

            var desaralizeToken = DesaralizeToken(token);

            if (token is null)
                return new UnauthorizedAccess();

            var TokenResult = await _unitOfWork.UserRepository.AddOrUpdateToken(desaralizeToken, userResult.Id);

            return token;
        }

        internal string DesaralizeToken(ResponseTokenDTO responseTokenDTO)
        {
            return responseTokenDTO.Bearer;
        }

       
    }
}
