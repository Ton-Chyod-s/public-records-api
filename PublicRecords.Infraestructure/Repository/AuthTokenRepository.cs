using PublicRecords.Domain.Entities.Token;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;

namespace PublicRecords.Infraestructure.Repository
{
    internal class AuthTokenRepository(OfficialDiaryDbContext context) : BaseRepository<AuthToken>(context), IAuthTokenRepository
    {
        public async Task<bool> AddOrUpdateAuthToken(long authToken, string token, long userId)
        {
            var bearer =  _context.AuthToken
            .FirstOrDefault(x => x.Id == authToken);

            if (bearer is null)
            { 
                var newBearer = new AuthToken(token, userId);
                await _context.AuthToken.AddAsync(newBearer);
            } else
            {
                bearer.UpdateBearer(token);
                _context.AuthToken.Update(bearer);
            }

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
