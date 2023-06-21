using BookerApi.Config;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookerApi.Lib
{
    public static class TokenService
    {
        public static string GenerateAccessToken(string uid)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, uid)
            };

            var expiration = DateTime.UtcNow.AddMinutes(30);

            return GenerateToken(claims, expiration, Secrets.JwtAccessSecret);
        }

        private static string GenerateToken(List<Claim> claims, DateTime expiration, string secret)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
              issuer: Secrets.JwtIssuer,
              expires: expiration,
              claims: claims,
              signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }
    }
}