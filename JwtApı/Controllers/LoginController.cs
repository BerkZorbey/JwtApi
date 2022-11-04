using JwtApı.Models;
using JwtApı.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApı.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public LoginController(TokenService tokenService)
        {
                _tokenService = tokenService;
        }
        [HttpPost]
        public Token Get([FromBody]User user)
        {
            Token token=new Token();
            if(user.Email == "a@a.com" && user.Password == "1")
            {
                token = _tokenService.TokenGenerator(user);
            }
            return token;
        }
    }
}
