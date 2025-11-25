using AutoMapper;
using Microservice_Net9_.Order.Application.Dtos;
using Microservice_Net9_.Order.Domain.Entities;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Application.Features.Orders
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
