using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Core.Utils;
using Doctorla.Dto.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Business.Helpers.Token
{
    public static class TokenCreator
    {
        public static TokenDto CreateToken(long id, string email, AccountType accountType, TokenOptions tokenOptions)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecretKey(accountType, tokenOptions));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ClaimUtils.CreateClaims(id, email, accountType)),
                Expires = DateTime.UtcNow.AddMinutes(tokenOptions.AccessTokenLifetime),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var refreshToken = Guid.NewGuid().ToString();
            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private static string GetSecretKey(AccountType accountType, TokenOptions tokenOptions)
        {
            switch (accountType)
            {
                case AccountType.Admin:
                    return tokenOptions.AdminSecretKey;
                case AccountType.User:
                    return tokenOptions.UserSecretKey;
                case AccountType.Doctor:
                    return tokenOptions.DoctorSecretKey;
                case AccountType.Hospital:
                    return tokenOptions.HospitalSecretKey;
                default:
                    return "notfound";
            }
        }
    }
}
