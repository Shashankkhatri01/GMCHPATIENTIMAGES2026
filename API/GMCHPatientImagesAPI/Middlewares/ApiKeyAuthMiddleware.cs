using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1️⃣ Get headers
        var apiKey = context.Request.Headers["x-api-key"].FirstOrDefault();
        var timestamp = context.Request.Headers["x-timestamp"].FirstOrDefault();

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(timestamp))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing x-api-key or x-timestamp");
            return;
        }

        // 2️⃣ Validate API key
        var allowedKeys = _config.GetSection("ApiClients").Get<string[]>();
        if (!allowedKeys.Contains(apiKey))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Invalid API key");
            return;
        }

        // 3️⃣ Validate timestamp (must be within ±5 minutes)
        if (!DateTime.TryParse(timestamp, out DateTime requestTimeUtc))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid timestamp format");
            return;
        }

        var currentUtc = DateTime.UtcNow;
        if (Math.Abs((currentUtc - requestTimeUtc).TotalMinutes) > 5)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Request timestamp out of range");
            return;
        }

        // ✅ Passed all checks
        await _next(context);
    }
}