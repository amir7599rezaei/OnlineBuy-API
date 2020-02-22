using Microsoft.IdentityModel.Tokens;
using OnlineBuy.Service.JWT.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineBuy.Service.JWT.Implement
{
    public class JsonWebTokensService : IJsonWebTokensService
    {
        public string CreateToken(string keyValue, Claim[] payLoad, DateTime duration)
        {

            //   var claim = new[]
            //{
            //       new Claim(ClaimTypes.NameIdentifier, user.Id),
            //       new Claim(ClaimTypes.Name, user.UserName),
            //   };

            //create key jwt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            var credits = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(payLoad),
                Expires = duration,
                SigningCredentials = credits
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}
