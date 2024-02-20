using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPi_Example.Middlewares
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            context.Response.Headers.Add("CabecalhoCustomizado", $"{DateTime.Now:yyyy-MM-ddHH:mm:ss} - IP:{ipAddress}");

            await _next(context);
    
        }
    }

    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderMiddleware>();
        }
    }
}
