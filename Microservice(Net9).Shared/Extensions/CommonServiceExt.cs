using FluentValidation;
using FluentValidation.AspNetCore;
using Microservice_Net9_.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice_Net9_.Shared.Extensions
{
    public static class CommonServiceExt
    {
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services, Type assembly)
        {
            services.AddHttpContextAccessor();
            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));
            
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining(assembly);
            services.AddScoped<IIdentityService, IdentityServiceFake>();

            services.AddAutoMapper(assembly);
            

            return services;
        }
    }
}
