using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.Entities.User;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserByName(string name);
        Task<OneOf<bool, BaseError>> AddOrUpdateToken(string bearerToken, long userId);
        Task<OneOf<bool, BaseError>> AddUser(ResquestAddOrLoginDTO content);
        Task<OneOf<bool, BaseError>> UpdateUser(string name, RequestUpdateLoginDTO requestUpdateLoginDTO);
        Task<OneOf<bool, BaseError>> DeleteUser(long userId);
    }
}
