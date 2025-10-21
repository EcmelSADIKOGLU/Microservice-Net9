using Microsoft.Extensions.Options;

namespace Microservice_Net9_.Discount.Api.Options
{
    public static class OptionExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);

            return services;    
        }


    }
}
