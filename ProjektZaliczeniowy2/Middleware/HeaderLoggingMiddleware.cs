﻿namespace ProjektZaliczeniowy2.Middleware
{
    public class HeaderLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("=== NAGŁÓWKI ŻĄDANIA ===");
            foreach (var header in context.Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-Powered-By", "ProjektZaliczeniowy2");
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
