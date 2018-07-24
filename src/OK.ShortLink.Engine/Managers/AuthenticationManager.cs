using Microsoft.IdentityModel.Tokens;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OK.ShortLink.Engine.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserManager _userManager;

        public AuthenticationManager(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public string CreateToken(int userId, string issuer, string key, int expiresInMs)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, userId.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddMilliseconds(expiresInMs),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel VerifyPrincipal(ClaimsPrincipal principal)
        {
            string userIdString = null;

            if (principal.HasClaim(claim => claim.Type == JwtRegisteredClaimNames.Jti))
            {
                userIdString = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            }

            int userId;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out userId))
            {
                return null;
            }

            UserModel user = _userManager.GetUserById(userId);

            if (user == null || user.IsActive == false)
            {
                return null;
            }

            return user;
        }
    }
}