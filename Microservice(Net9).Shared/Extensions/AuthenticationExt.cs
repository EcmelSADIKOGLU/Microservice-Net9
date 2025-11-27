using Microservice_Net9_.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice_Net9_.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
            // Sign   ==> 
            // Aud    ==> payment.api   (Appsettings'den okunacak)
            // Issuer ==> http://localhost:8080/realms/microserviceTenant (Appsettings'den okunacak)
            // TTL (Token Lifetime) ==> Token'da var.

            var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = identityOptions.Address;
                    options.Audience = identityOptions.Audience;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };  
                });

            services.AddAuthorization();
            return services;
        }
    }
}
