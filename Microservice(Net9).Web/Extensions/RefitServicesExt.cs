using Microservice_Net9_.Web.DelegateHandlers;
using Microservice_Net9_.Web.Options;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.Extensions.Options;
using Refit;

namespace Microservice_Net9_.Web.Extensions
{
    public static class RefitServicesExt
    {
        public static IServiceCollection AddRefitServicesExt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
            {
                var microserviceOption = configuration.GetRequiredSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();

                configure.BaseAddress = new Uri(microserviceOption!.Catalog.BaseAddress);
            })
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()         
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            services.AddRefitClient<IBasketRefitService>().ConfigureHttpClient(configure =>
            {
                var microserviceOption = configuration.GetRequiredSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();

                configure.BaseAddress = new Uri(microserviceOption!.Basket.BaseAddress);
            })
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();

            return services;
        }
    }
}
