using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Healthy.Core.Middlewares
{
    internal class HttpPanelOutputMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpPanelOutputMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            return context.Response.WriteAsync("DUPA");
            // Call the next delegate/middleware in the pipeline
            // return this._next(context);
        }
    }
}