using MassTransit;
using Microservice_Net9_.Basket.Api.Features.Basket;
using Microservice_Net9_.Bus.Events;
using System.Text.Json;
using System.Threading;
using _Basket = Microservice_Net9_.Basket.Api.Data.Basket;

namespace Microservice_Net9_.Basket.Api.Consumers
{
    public class OrderCreatedEventConsumer(BasketService basketService) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            await basketService.DeleteBasket(context.Message.BuyerId);
        }
    }
}
