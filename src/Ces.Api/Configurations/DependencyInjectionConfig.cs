using Ces.Api.Business.Interfaces;
using Ces.Api.Business.Notificacoes;
using Ces.Api.Domain;
using Ces.Api.Infrastructure.Context;
using Ces.Api.Infrastructure.Repository;
using Ces.Api.Business.Models;

namespace Ces.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<CesContext>();
            services.AddScoped<ITipoPerguntaRepository, TipoPerguntaRepository>();
            services.AddScoped<IPerguntaRepository, PerguntaRepository>();
            services.AddScoped<IRespostaDomain, RespostaDomain>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPerguntaDomain, PerguntaDomain>();
            services.AddScoped<ITipoPerguntaDomain, TipoPerguntaDomain>();

            return services;
        }



    }
}
