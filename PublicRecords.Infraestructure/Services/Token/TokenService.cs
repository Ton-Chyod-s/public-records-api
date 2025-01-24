using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Entities.User;
using PublicRecords.Domain.Interface.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PublicRecords.Infraestructure.Services.Token
{
    internal class TokenService
        (
            IConfiguration configuration
        ) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;

        public ResponseTokenDTO GenerateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));

            var issuer = _configuration["Jwt:Issuer"] ?? string.Empty;

            var audience = _configuration["Jwt:Audience"] ?? string.Empty;

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Roles.ToString()!)
                },
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new ResponseTokenDTO(token);
        }

    }
}
