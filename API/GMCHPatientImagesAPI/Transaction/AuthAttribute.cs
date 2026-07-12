using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var request = context.HttpContext.Request;

        var apiKey = request.Headers["x-api-key"].FirstOrDefault();
        var secretKey = request.Headers["x-secret-key"].FirstOrDefault();
        var timestamp = request.Headers["x-timestamp"].FirstOrDefault();

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(timestamp))
        {
            context.Result = new ContentResult
            {
                StatusCode = 401,
                Content = "Missing x-api-key, x-secret-key, or x-timestamp"
            };
            return;
        }

        // 🔹 Load configured clients
        var clientsSection = config.GetSection("ApiClients").GetChildren();

        // 🔹 Match both API key and secret key
        var matchedClient = clientsSection.FirstOrDefault(c =>
            string.Equals(c.GetValue<string>("ApiKey"), apiKey, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(c.GetValue<string>("SecretKey"), secretKey, StringComparison.OrdinalIgnoreCase)
        );

        if (matchedClient == null)
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = "Invalid API key or secret key"
            };
            return;
        }

        // 🔹 Validate timestamp within ±5 minutes
        if (!DateTime.TryParseExact(
                timestamp,
                new[] { "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fffZ", "o" },
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal,
                out var requestTimeUtc))
        {
            context.Result = new ContentResult
            {
                StatusCode = 400,
                Content = "Invalid timestamp format"
            };
            return;
        }

        var currentUtc = DateTime.UtcNow;
        if (Math.Abs((currentUtc - requestTimeUtc).TotalMinutes) > 5)
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = "Request timestamp out of range"
            };
            return;
        }

        // 🔹 (Commented) Optional IP validation — enable later
        /*
        var remoteIp = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        var allowedIps = matchedClient.GetSection("AllowedIps").Get<string[]>() ?? Array.Empty<string>();
        if (allowedIps.Length > 0 && !allowedIps.Contains(remoteIp))
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = $"IP {remoteIp} not allowed"
            };
            return;
        }
        */

        // 🔹 (Commented) Optional Domain/Subdomain validation
        /*
        var origin = request.Headers["Origin"].FirstOrDefault();
        if (!string.IsNullOrEmpty(origin))
        {
            var allowedDomains = matchedClient.GetSection("AllowedDomains").Get<string[]>() ?? Array.Empty<string>();
            if (allowedDomains.Length > 0 && !allowedDomains.Any(d => origin.Contains(d, StringComparison.OrdinalIgnoreCase)))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Domain not allowed"
                };
                return;
            }
        }
        */

        // ✅ All checks passed
        await next();
    }
}
