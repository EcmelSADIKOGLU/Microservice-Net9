using Microservice_Net9_.Web.DelegateHandlers;
using Microservice_Net9_.Web.Options;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;

namespace Microservice_Net9_.Web.Extensions
{
    public static class AuthenticationConfigurationExt
    {
        public static IServiceCollection AddAuthenticationConfigurationExt(this IServiceCollection services)
        {
            services.AddAuthentication(configureOption =>
            {
                configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Auth/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.Cookie.Name = "Microservice.Web.Cookie";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

            

            return services;
        }
    }
}
