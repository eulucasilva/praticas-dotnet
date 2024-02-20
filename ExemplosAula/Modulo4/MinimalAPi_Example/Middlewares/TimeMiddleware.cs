using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPi_Example.Middlewares
{
    public class TimeMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var time = new Stopwatch();
            time.Start();

            await _next(context);

            time.Stop();
            await context.Response.WriteAsync($"\n\nDuracao da solitacao para {context.Request.Path}: {time.ElapsedMilliseconds} ms e {time.ElapsedMilliseconds*1000} µs");

        }
    }

    public static class TimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMiddleware>();
        }
    }
}
