using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ces.Api.HealthCheck
{
    public static class HealthChecksConfiguration
    {
        public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }

        public static void UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                ResponseWriter = CustomResponseWriter.Write
            });
        }
    }
}
