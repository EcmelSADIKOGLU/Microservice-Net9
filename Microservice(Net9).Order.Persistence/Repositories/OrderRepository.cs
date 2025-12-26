using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) : GenericRepository<_Order, Guid>(context), IOrderRepository
    {
        public Task<List<_Order>> GetOrdersByUserId(Guid buyerId)
        {
            return context.Orders
                .Where(o => o.BuyerId == buyerId)
                .Include(x => x.OrderItems)
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();
        }

        public Task<List<_Order>> GetSuspendedOrders()
        {
            return context.Orders
                .Where(o => o.Status == OrderStatus.WaitingForPayment)
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();
        }

        public async Task SetStatus(string orderCode, Guid paymentId, OrderStatus status)
        {
            var order = context.Orders.First(o => o.OrderCode == orderCode);
            order.Status = status;
            order.PaymentId = paymentId;
            context.Update(order);

        }
    }
}
