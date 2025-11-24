using MediatR;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Domain.Entities;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using System.Net;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(
        IGenericRepository<_Order, Guid> orderRepository,
        IGenericRepository<Address, int> addressRepository,
        IIdentityService identityService
        ) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!request.OrderItems.Any())
            {
                return Task.FromResult(ServiceResult.Error("Order Item not found", "Order must have at least one item", HttpStatusCode.BadRequest));
            }
            //TODO: transaction başlatılacak
            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            addressRepository.Add(newAddress);
            //TODO: unit of work commit
            
            var order = _Order.CreateUnpaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);


            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
            }
            orderRepository.Add(order);
            //TODO: unit of work commit

            var paymentId = Guid.Empty;
            //TODO: Payment

            order.MarkAsPaid(paymentId);
            orderRepository.Update(order);
            //TODO: unit of work commit

            return Task.FromResult(ServiceResult.SuccessAsNoContent());


        }
    }
}
