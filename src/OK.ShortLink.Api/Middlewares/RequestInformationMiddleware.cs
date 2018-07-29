using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OK.ShortLink.Api.Middlewares
{
    public class RequestInformationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestInformationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Items.Add("RequestId", Guid.NewGuid().ToString());
            context.Items.Add("IPAddress", context.Connection.RemoteIpAddress.MapToIPv4().ToString());
            context.Items.Add("UserAgent", context.Request.Headers["User-Agent"].FirstOrDefault());

            await _next.Invoke(context);
        }
    }
}