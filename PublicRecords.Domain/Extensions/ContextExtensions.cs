using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace PublicRecords.Domain.Extensions
{
    public static class ContextExtensions
    {
        public static string GetTokenByContext(this HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeaders))
                return string.Empty;

            return authHeaders.FirstOrDefault()?.Split(" ").Last() ?? string.Empty;
        }

        public static string GetSystemIdentifierIdByContext(this HttpContext httpContext)
        {
            string token = httpContext.GetTokenByContext();

            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            return GetTokenIdentifierClaim(token);
        }

        public static string GetTokenIssuerFromHttpContext(this HttpContext httpContext)
        {
            string token = httpContext.GetTokenByContext();

            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            return GetIssuerClaimFromToken(token);
        }

        public static string GetUserIdByContext(this HttpContext httpContext)
        {
            string token = httpContext.GetTokenByContext();

            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            return GetIdClaimFromToken(token);
        }

        private static string GetClaimValueFromToken(string token, string claimType)
        {
            var claims = DeserializeJwtToken(token);

            if (claims == null || claims.Length == 0) return string.Empty;

            string? claimValue = claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            return string.IsNullOrEmpty(claimValue) ? string.Empty : claimValue;
        }

        private static string GetTokenIdentifierClaim(string token)
        {
            return GetClaimValueFromToken(token, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
        }

        private static string GetIssuerClaimFromToken(string token)
        {
            return GetClaimValueFromToken(token, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        }

        private static string GetIdClaimFromToken(string token)
        {
            return GetClaimValueFromToken(token, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        }
        private static Claim[]? DeserializeJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            IEnumerable<Claim> claims = jwtToken.Claims;

            return claims.ToArray();
        }
    }
}
