using Microsoft.AspNetCore.Http;
using OK.ShortLink.Common.Models;
using OK.ShortLink.Core.Managers;
using System.Threading.Tasks;

namespace OK.ShortLink.Api.Middlewares
{
    public class UserIdentifierMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthenticationManager authenticationManager)
        {
            UserModel authenticatedUser = authenticationManager.VerifyPrincipal(context.User);

            context.Items.Add("UserId", authenticatedUser?.Id);

            await _next.Invoke(context);
        }
    }
}