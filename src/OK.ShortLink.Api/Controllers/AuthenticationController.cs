using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OK.ShortLink.Api.Requests;
using OK.ShortLink.Api.Responses;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;

namespace OK.ShortLink.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserManager userManager, IAuthenticationManager authenticationManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate([FromBody] AuthenticationRequest request)
        {
            UserModel user = _userManager.LoginUser(request.Username, request.Password);

            if (user == null)
            {
                return NotFound();
            }

            AuthenticationResponse response = new AuthenticationResponse();

            response.Code = 200;
            response.Message = "The user is authenticated successfuly.";
            response.Token = CreateUserToken(user);
            response.ExpiresIn = int.Parse(_configuration["Jwt:ExpiresInMs"]);

            return response;
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