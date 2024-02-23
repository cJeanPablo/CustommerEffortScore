using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Ces.Api.HealthCheck;
using Ces.Api.Configurations;
using Ces.Api.Infrastructure.Context;

namespace Ces.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<CesContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("DefaultConnection:ConnectionString").Get<string>());
            });
            services.AddHttpContextAccessor();
            services.AddHealthChecksConfiguration();
            services.AddApiConfig();
            services.AddSwaggerConfig();
            services.ResolveDependencias();
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.EnvironmentName != "PRD")
            {
                app.UseSwaggerConfig(provider);
            }
            app.UseApiConfig(env);
            app.UseHealthChecksConfiguration();
        }

    }
}
