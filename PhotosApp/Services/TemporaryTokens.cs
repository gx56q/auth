using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PhotosApp.Services
{
    public static class TemporaryTokens
    {
        public const string CookieName = "TemporaryToken";

        public static SymmetricSecurityKey SigningKey => new(Encoding.ASCII.GetBytes("Ne!0_0!vzlomayesh!^_^!nikogda!"));

        public static string GenerateEncoded()
        {
            var claims = new Claim[]
            {
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: null,
                expires: null,
                signingCredentials: null);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}