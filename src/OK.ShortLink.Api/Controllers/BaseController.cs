using Microsoft.AspNetCore.Mvc;
using OK.ShortLink.Core.Managers;

namespace OK.ShortLink.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected int? CurrentUserId
        {
            get
            {
                IAuthenticationManager authenticationManager = (IAuthenticationManager)HttpContext.RequestServices.GetService(typeof(IAuthenticationManager));

                return authenticationManager.GetUserIdByPrincipal(HttpContext.User);
            }
        }
    }
}