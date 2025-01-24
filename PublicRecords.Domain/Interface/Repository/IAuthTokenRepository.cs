using PublicRecords.Domain.Entities.Token;

namespace PublicRecords.Domain.Interface.Repository
{
    public interface IAuthTokenRepository : IBaseRepository<AuthToken>
    {
        Task<bool> AddOrUpdateAuthToken(long authToken, string token, long userId);
    }
}
