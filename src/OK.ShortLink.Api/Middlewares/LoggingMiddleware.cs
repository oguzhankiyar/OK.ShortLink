using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using OK.ShortLink.Core.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OK.ShortLink.Api.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LoggingMiddleware(ILogger logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();

            using (MemoryStream requestBodyStream = new MemoryStream())
            {
                using (MemoryStream responseBodyStream = new MemoryStream())
                {
                    Stream originalRequestBody = context.Request.Body;
                    context.Request.EnableRewind();
                    Stream originalResponseBody = context.Response.Body;

                    string method = context.Request.Method;
                    string url = context.Request.GetDisplayUrl();

                    try
                    {
                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        context.Request.Body = requestBodyStream;
                        context.Response.Body = responseBodyStream;

                        _logger.SetThreadProperty("RequestId", Guid.NewGuid().ToString());
                        _logger.SetThreadProperty("IPAddress", context.Connection.RemoteIpAddress.MapToIPv4().ToString());
                        _logger.SetThreadProperty("UserAgent", context.Request.Headers["User-Agent"].FirstOrDefault());

                        if (string.IsNullOrEmpty(requestBodyText))
                        {
                            _logger.Debug("LoggingMiddleware/RequestStarted", $"{method} {url}");
                        }
                        else
                        {
                            _logger.DebugData("LoggingMiddleware/RequestStarted", $"{method} {url}", new
                            {
                                Body = requestBodyText
                            });
                        }

                        watch.Start();

                        await _next(context);

                        watch.Stop();

                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                        string responseBodyText = new StreamReader(responseBodyStream).ReadToEnd();

                        if (string.IsNullOrEmpty(responseBodyText))
                        {
                            _logger.DebugData("LoggingMiddleware/RequestCompleted", $"{method} {url}", new
                            {
                                Code = context.Response.StatusCode,
                                ElapsedTime = watch.ElapsedMilliseconds
                            });
                        }
                        else
                        {
                            _logger.DebugData("LoggingMiddleware/RequestCompleted", $"{method} {url}", new
                            {
                                Code = context.Response.StatusCode,
                                Body = responseBodyText,
                                ElapsedTime = watch.ElapsedMilliseconds
                            });
                        }

                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                        await responseBodyStream.CopyToAsync(originalResponseBody);
                    }
                    catch (Exception ex)
                    {
                        if (watch.IsRunning)
                            watch.Stop();

                        _logger.Error("LoggingMiddleware/RequestFailed", ex);

                        _logger.DebugData("LoggingMiddleware/RequestCompleted", $"{method} {url}", new
                        {
                            Code = context.Response?.StatusCode,
                            ElapsedTime = watch.ElapsedMilliseconds
                        });

                        throw ex;
                    }
                    finally
                    {
                        context.Request.Body = originalRequestBody;
                        context.Response.Body = originalResponseBody;
                    }
                }
            }
        }
    }
}