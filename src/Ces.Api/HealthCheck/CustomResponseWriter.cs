using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ces.Api.Configurations;
using Ces.Api.Middleware;
using System.Net.Mime;
using System.Text.Json;

namespace Ces.Api.HealthCheck
{
    public class CustomResponseWriter
    {
        public static Task Write(HttpContext context, HealthReport result)
        {
            string json = JsonSerializer.Serialize(new HealthCheckResponse(result.Status.ToString()), new JsonConfiguration().Get());
            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }
    }
}
