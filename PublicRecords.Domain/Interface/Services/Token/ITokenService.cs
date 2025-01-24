using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Entities.User;

namespace PublicRecords.Domain.Interface.Services.Token
{
    public interface ITokenService
    {
        ResponseTokenDTO GenerateToken(User user);
    }
}
