using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.Login
{
    public interface IDeleteLoginUseCase
    {
        Task<OneOf<bool, BaseError>> DeleteUser();
    }
}
