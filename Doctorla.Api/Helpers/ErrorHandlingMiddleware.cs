using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using Doctorla.Core.Enums;
using Doctorla.Core.Exceptions;

namespace Doctorla.Api.Helpers
{
    /// <summary>
    /// Error handling middleware that is injected into .NET HTTP pipeline
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// ctor
        /// </summary>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Each request passes this method
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = ErrorCode.InternalServerError;
            switch (exception)
            {
                case PermissionDeniedException:
                    errorCode = ErrorCode.PermissionDenied;
                    break;
                default:
                    break;
            }

            var headers = context.Request.Headers;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Headers");
            foreach (var header in headers)
            {
                sb.AppendLine($"*{header.Key} - {header.Value}");
            }
            var stream = new StreamReader(context.Request.Body);
            stream.BaseStream.Seek(0, SeekOrigin.Begin);
            var body = await stream.ReadToEndAsync();
            sb.AppendLine("Body");
            sb.AppendLine(body);
            //await ErrorLogger.Log(sb.ToString(), exception.Message, exception.StackTrace, exception.InnerException?.Message,
            //    exception.InnerException?.StackTrace, context.Request.Path);

            //var result = JsonConvert.SerializeObject(Result<object>.CreateErrorResult(errorCode));
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.OK;
            //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //await context.Response.WriteAsync(result);
        }
    }
}
