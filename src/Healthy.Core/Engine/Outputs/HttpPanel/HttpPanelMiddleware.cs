using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Core.Engine.Outputs.HttpPanel
{
    public class HttpPanelMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        const string BlacklistedHost = "mickl.net";

        public HttpPanelMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues referer;
            if (context.Request.Headers.TryGetValue("Referer", out referer))
            {
                var host = context.Request.Host;
                var refererValue = referer.First();
                if (!refererValue.Contains(host.ToString()))
                {
                    _logger.LogInformation("We're linked from: ", refererValue);
                    if (refererValue.Contains(BlacklistedHost))
                    {
                        context.Response.StatusCode = 403;
                        return;
                    }
                }
            }

            await _next.Invoke(context);
        }
    }
}
