using OK.ShortLink.Common.Models;
using System.Security.Claims;

namespace OK.ShortLink.Core.Managers
{
    public interface IAuthenticationManager
    {
        string CreateToken(int userId, string issuer, string key, int expiresInMs);

        int? GetUserIdByPrincipal(ClaimsPrincipal principal);

        UserModel VerifyPrincipal(ClaimsPrincipal principal);
    }
}