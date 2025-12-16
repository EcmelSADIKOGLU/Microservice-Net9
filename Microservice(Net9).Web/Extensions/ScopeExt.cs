using Microservice_Net9_.Web.DelegateHandlers;
using Microservice_Net9_.Web.Options;
using Microservice_Net9_.Web.Pages.Auth.SignIn;
using Microservice_Net9_.Web.Pages.Auth.SignUp;
using Microservice_Net9_.Web.Services;
using Microservice_Net9_.Web.Services.Refit;
using Refit;

namespace Microservice_Net9_.Web.Extensions
{
    public static class ScopeExt
    {
        public static IServiceCollection AddScopeExt(this IServiceCollection services)
        {
            services.AddHttpClient<SignUpService>();
            services.AddHttpClient<SignInService>();

            services.AddScoped<TokenService>();
            services.AddScoped<UserService>();

            services.AddScoped<CatalogService>();
            services.AddScoped<BasketService>();
            services.AddScoped<DiscountService>();


            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();

            return services;
        }
    }
}
