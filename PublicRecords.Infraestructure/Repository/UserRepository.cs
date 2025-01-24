using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.Entities.Token;
using PublicRecords.Domain.Entities.User;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace PublicRecords.Infraestructure.Repository
{
    internal class UserRepository(OfficialDiaryDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetUserByName(string name)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<OneOf<bool, BaseError>> AddUser(ResquestAddOrLoginDTO content)
        {
            var findUser = new User(content.UserName,content.Password);
            await _context.User.AddAsync(findUser);

            if (await _context.SaveChangesAsync() <= 0)
                return new UserNotSaved();

            return true;
        }

        public async Task<OneOf<bool, BaseError>> UpdateUser(string userName, RequestUpdateLoginDTO requestUpdateLoginDTO)
        {
            var findUser = await _context.User.FirstOrDefaultAsync(x => x.UserName == userName);

            if (findUser is null)
                return new UserNotUpdate();

            findUser.UpdateUser(requestUpdateLoginDTO.Name, true, requestUpdateLoginDTO.Type);
            _context.User.Update(findUser);

            if (await _context.SaveChangesAsync() <= 0)
                return new UserNotUpdate();

            return true;
        }

        public async Task<OneOf<bool, BaseError>> DeleteUser(long userId)
        {
            var findUser = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);

            if (findUser is null)
                return new UserNotFound();

            _context.User.Remove(findUser);

            if (await _context.SaveChangesAsync() <= 0)
                return new UserNotFound();

            return true;
        }

        public async Task<OneOf<bool, BaseError>> AddOrUpdateToken(string bearerToken, long userId)
        {
            var token = _context.AuthToken.FirstOrDefault(x => x.UserId == userId);

            if (token is null)
            {
                token = new AuthToken(bearerToken, userId);
                await _context.AuthToken.AddAsync(token);

            }
            else
            {
                token.UpdateBearer(bearerToken);
                _context.AuthToken.Update(token);
            }

            return await _context.SaveChangesAsync() < 0 ? true : new TokenNotSaved();
        }
    }
}
