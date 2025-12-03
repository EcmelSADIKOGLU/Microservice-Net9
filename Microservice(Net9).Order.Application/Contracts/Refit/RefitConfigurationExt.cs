using Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService;
using Microservice_Net9_.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Microservice_Net9_.Order.Application.Contracts.Refit
{
    public static class RefitConfigurationExt
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
            {
                var addressUrlOption = configuration.GetRequiredSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();

                configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

            return services;
        }
    }
}
