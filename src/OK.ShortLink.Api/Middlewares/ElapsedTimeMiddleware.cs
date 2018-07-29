using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OK.ShortLink.Api.Middlewares
{
    public class ElapsedTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ElapsedTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            await _next(context);

            watch.Stop();

            context.Items.Add("ElapsedTime", watch.ElapsedMilliseconds);
        }
    }
}