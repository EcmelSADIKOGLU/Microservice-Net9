using AutoMapper;
using MassTransit;
using Microservice_Net9_.Bus.Events;
using Microservice_Net9_.Discount.Api.Features;
using Microservice_Net9_.Discount.Api.Repositories;
using System;
using System.Threading;
using _Discount = Microservice_Net9_.Discount.Api.Features.Discounts.Discount;

namespace Microservice_Net9_.Discount.Api.Consumers
{
    public class OrderCreatedEventConsumer(AppDbContext dbContext) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {

            var discount = new _Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = DiscountCodeGenerator.Generate(),
                CreateTime = DateTime.UtcNow,   
                ExpireTime = DateTime.UtcNow.AddMonths(1),
                Rate = 0.1f,
                UserId = context.Message.BuyerId
            };

            await dbContext.Discounts.AddAsync(discount);

            await dbContext.SaveChangesAsync();
        }
    }
}
