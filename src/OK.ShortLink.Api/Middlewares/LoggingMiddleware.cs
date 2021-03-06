﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using OK.ShortLink.Core.Logging;
using System.IO;
using System.Text;
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
            SetLoggerThreadProperties(context);

            string method = context.Request.Method;
            string url = context.Request.GetDisplayUrl();

            string requestBodyText = await ReadRequestBody(context.Request);

            LogRequest(method, url, requestBodyText);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                string responseBodyText = await ReadResponseBody(context.Response);
                long elapsedTime = (long)context.Items["ElapsedTime"];

                LogResponse(method, url, context.Response.StatusCode, responseBodyText, elapsedTime);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        #region Helpers

        private void SetLoggerThreadProperties(HttpContext context)
        {
            _logger.SetThreadProperty("UserId", context.Items["UserId"]);
            _logger.SetThreadProperty("RequestId", context.Items["RequestId"]);
            _logger.SetThreadProperty("IPAddress", context.Items["IPAddress"]);
            _logger.SetThreadProperty("UserAgent", context.Items["UserAgent"]);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            using (StreamReader reader = new StreamReader(request.Body))
            {
                string bodyAsText = await reader.ReadToEndAsync();
                
                MemoryStream requestBodyStream = new MemoryStream();
                byte[] bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                await requestBodyStream.WriteAsync(bytesToWrite, 0, bytesToWrite.Length);
                requestBodyStream.Seek(0, SeekOrigin.Begin);

                request.Body = requestBodyStream;

                return bodyAsText;
            }
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }

        private void LogRequest(string method, string url, string requestBodyText)
        {
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
        }

        private void LogResponse(string method, string url, int statusCode, string responseBodyText, long elapsedTime)
        {
            if (string.IsNullOrEmpty(responseBodyText))
            {
                _logger.DebugData("LoggingMiddleware/RequestCompleted", $"{method} {url}", new
                {
                    Code = statusCode,
                    ElapsedTime = elapsedTime
                });
            }
            else
            {
                _logger.DebugData("LoggingMiddleware/RequestCompleted", $"{method} {url}", new
                {
                    Code = statusCode,
                    Body = responseBodyText,
                    ElapsedTime = elapsedTime
                });
            }
        }

        #endregion
    }
}