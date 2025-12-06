using Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService;
using Microservice_Net9_.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace Microservice_Net9_.Order.Application.Contracts.Refit
{
    public static class RefitConfigurationExt
    {
        public static IServiceCollection AddRefitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();

            services.AddOptions<IdentityOption>()
                .BindConfiguration(nameof(IdentityOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IdentityOption>(
                sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);


            services.AddOptions<ClientSecretOption>()
                .BindConfiguration(nameof(ClientSecretOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<ClientSecretOption>(
                sp => sp.GetRequiredService<IOptions<ClientSecretOption>>().Value);


            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
            {
                var addressUrlOption = configuration.GetRequiredSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();

                configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
            })
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()         //Password
            .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();  //ClientCredential

            return services;
        }
    }
}
