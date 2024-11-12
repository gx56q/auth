﻿using System;
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
                new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, "Temporary Dev"),
                new(ClaimsIdentity.DefaultRoleClaimType, "Dev")
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: now.AddSeconds(30),
                signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}