using System;
using CaioOliveira.IBGE.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CaioOliveira.IBGE.Configuration
{
    public static class Configuration
    {
        public static void UseIbgeService(this IServiceCollection services)
        {
            var configuration = new ServiceConfiguration
            {
                BaseApiUrl = "https://servicodados.ibge.gov.br/api/v1/"
            };
            
            Configure(services, configuration);
        }
        
        public static void UseIbgeService(this IServiceCollection services, Action<ServiceConfiguration> options)
        {
            var configuration = new ServiceConfiguration();
            options(configuration);
            
            Configure(services, configuration);
        }

        private static void Configure(IServiceCollection services, ServiceConfiguration configuration)
        {
            services.AddScoped<IIbgeService, Service>();
            services.Configure<ServiceConfiguration>(x => x.Bind(configuration));
        }
    }
}