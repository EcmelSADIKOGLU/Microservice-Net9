using MediatR;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Application.Contracts.UnitOfWork;
using Microservice_Net9_.Order.Domain.Entities;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using System.Net;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Application.UseCases.Orders.Create
{
    public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!request.OrderItems.Any())
            {
                return ServiceResult.Error("Order Item not found", "Order must have at least one item", HttpStatusCode.BadRequest);
            }

            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            var order = _Order.CreateUnpaidOrder(identityService.UserId, request.DiscountRate);


            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice);
            }
            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);


            //TODO: Payment
            var paymentId = Guid.Empty;


            order.MarkAsPaid(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();


        }
    }
}
