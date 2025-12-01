using MassTransit;
using Microservice_Net9_.Bus;
using Microservice_Net9_.Bus.Commands;
using Microservice_Net9_.Discount.Api.Consumers;

namespace Microservice_Net9_.Discount.Api
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
IConfiguration configuration)
        {
            var busOptions = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;

            services.AddMassTransit(configure =>
            {
                //TODO: Reflaction ile Common ext kullan ve bunu kullanmana gerek kalmasın.
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });


                    //cfg.ConfigureEndpoints(ctx); // oto named queue

                    cfg.ReceiveEndpoint("discount-microservice.order-created-event.queue",
                        e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });
                });
            });


            return services;
        }
    }
}
