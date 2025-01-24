using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Login
{
    public interface ICreateLoginUseCase
    {
        Task<OneOf<bool, BaseError>> CreateLogin(ResquestAddOrLoginDTO content);
    }
}
