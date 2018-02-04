﻿using Healthy.Core.Engine.HealthChecks;
using Healthy.Core.Engine.Storage;
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
    internal class HttpPanelMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHealthyEngine _healthyEngine;
        private readonly IHealthCheckResultStorage _storage;

        public HttpPanelMiddleware(RequestDelegate next, IHealthyEngine healthyEngine, IHealthCheckResultStorage storage)
        {
            _next = next;

            _healthyEngine = healthyEngine;
            _storage = storage;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            var healthChecks = _healthyEngine.HealthCheckEngine.HealthChecks;
            var sb = new StringBuilder();

            sb.Append("<ul>");
            foreach (var cheakthCheck in healthChecks)
            {
                sb.Append("<li><div>");
                sb.Append("<p>" + cheakthCheck.Name + "</p>");

                var a = await _storage.GetLastResultAsync(cheakthCheck.Id);
                if (a == HealthCheckResult.Empty)
                    sb.Append("<pre>No data</pre>");
                else
                    sb.Append("<pre>" + a + "</pre>");
                sb.Append("</div></li>");

            }

            sb.Append("</ul>");
            await context.Response.WriteAsync(sb.ToString());

            return;
        }
    }
}
