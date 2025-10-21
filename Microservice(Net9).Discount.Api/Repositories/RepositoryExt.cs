using Microservice_Net9_.Discount.Api.Options;
using MongoDB.Driver;

namespace Microservice_Net9_.Discount.Api.Repositories
{
    public static class RepositoryExt
    {
        public static IServiceCollection AddRepositoryExt(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var options = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(options.ConnectionString);

            });

            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();
                var mongoOptions = sp.GetRequiredService<MongoOptions>();

                return AppDbContext.Create(mongoClient.GetDatabase(mongoOptions.DatabaseName));

            });

            return services;
        }
    }
}
