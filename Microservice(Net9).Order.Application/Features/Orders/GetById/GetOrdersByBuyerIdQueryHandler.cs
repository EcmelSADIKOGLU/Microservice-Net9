using AutoMapper;
using MediatR;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Application.Dtos;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservice_Net9_.Order.Application.Features.Orders.GetById
{
    public class GetOrdersByBuyerIdQueryHandler(
        IOrderRepository orderRepository,
        IMapper mapper
        ) : IRequestHandler<GetOrdersByBuyerIdQuery, ServiceResult<GetOrdersByBuyerIdResponse>>
    {
        public async Task<ServiceResult<GetOrdersByBuyerIdResponse>> Handle(GetOrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrdersByUserId(request.BuyerId);
            var orderDtos = orders.Select(o => new OrderDto(o.CreatedDate, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();   
            var response = new GetOrdersByBuyerIdResponse(orderDtos);
            return ServiceResult<GetOrdersByBuyerIdResponse>.SuccessAsOk(response);
        }
    }
}

