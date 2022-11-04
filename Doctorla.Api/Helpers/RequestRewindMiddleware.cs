using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Doctorla.Api.Helpers
{
    /// <summary>
    /// Request rewinding middleware that is injected into .NET HTTP pipeline
    /// </summary>
    public class RequestRewindMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        public RequestRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Each request passes this method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
    }
}
