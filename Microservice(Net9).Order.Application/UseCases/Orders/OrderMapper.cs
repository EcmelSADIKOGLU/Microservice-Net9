using AutoMapper;
using Microservice_Net9_.Order.Application.Dtos;
using Microservice_Net9_.Order.Domain.Entities;

namespace Microservice_Net9_.Order.Application.UseCases.Orders
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
