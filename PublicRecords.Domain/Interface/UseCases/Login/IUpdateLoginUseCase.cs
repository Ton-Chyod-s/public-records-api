using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Login
{
    public interface IUpdateLoginUseCase
    {
        Task<OneOf<ResponseTokenDTO, BaseError>> UpdateLogin(RequestUpdateLoginDTO requestUpdateLoginDTO);
    }
}
