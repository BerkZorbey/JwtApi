using JwtApı.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApı.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;

        }

        public Token TokenGenerator(User user)
        {
            Token token = new Token();
            DateTime exp = DateTime.Now.AddHours(1);
            token.Expiration = exp;


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:_config["Jwt:Issuer"],
                audience:_config["Jwt:Audience"],
                notBefore: DateTime.Now,
                expires: exp,
                signingCredentials: signingCredentials,
                claims: new Claim[]
                {
                    new Claim("UserId",user.Id.ToString())
                }
                );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = Guid.NewGuid().ToString();
            return token;
        }
    }
}
