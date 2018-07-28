using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OK.ShortLink.Api.Requests;
using OK.ShortLink.Api.Responses;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;

namespace OK.ShortLink.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IConfiguration _configuration;

        public AuthController(IUserManager userManager, IAuthenticationManager authenticationManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateToken([FromBody] CreateTokenRequest request)
        {
            UserModel user = _userManager.LoginUser(request.Username, request.Password);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            CreateTokenResponse response = new CreateTokenResponse();

            response.Token = CreateUserToken(user);
            response.ExpiresIn = int.Parse(_configuration["Jwt:ExpiresInMs"]);

            return Ok(response);
        }

        #region Helpers

        private string CreateUserToken(UserModel user)
        {
            string issuer = _configuration["Jwt:Issuer"];
            string key = _configuration["Jwt:Key"];
            int expiresInMs = int.Parse(_configuration["Jwt:ExpiresInMs"]);

            return _authenticationManager.CreateToken(user.Id, issuer, key, expiresInMs);
        }

        #endregion
    }
}