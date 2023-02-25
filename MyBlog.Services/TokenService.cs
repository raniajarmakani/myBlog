using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyBlog.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyBlog.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly String _issuer;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            _issuer = config["JWT:Issuer"];

        }
            
        public string CreateToken(UserIdentity user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId , user.UserID.ToString()),
               new Claim(JwtRegisteredClaimNames.UniqueName , user.UserName)
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                _issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds

                ) ;
            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }
    }
}
