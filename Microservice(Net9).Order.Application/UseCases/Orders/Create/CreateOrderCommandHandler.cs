using MassTransit;
using MediatR;
using Microservice_Net9_.Bus.Events;
using Microservice_Net9_.Order.Application.Contracts.Refit.PaymentService;
using Microservice_Net9_.Order.Application.Contracts.Repositories;
using Microservice_Net9_.Order.Application.Contracts.UnitOfWork;
using Microservice_Net9_.Order.Domain.Entities;
using Microservice_Net9_.Shared;
using Microservice_Net9_.Shared.Services;
using System;
using System.Net;
using _Order = Microservice_Net9_.Order.Domain.Entities.Order;

namespace Microservice_Net9_.Order.Application.UseCases.Orders.Create
{
    public class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IIdentityService identityService,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        IPaymentService paymentService
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


            var createPaymentRequest = new CreatePaymentRequest(
                order.OrderCode,
                request.Payment.CardNumber,
                request.Payment.CardHolderName,
                request.Payment.ExpirationDate,
                request.Payment.Cvv,
                order.TotalPrice
                );

            CreatePaymentResponse createPaymentResponse = await paymentService.CreateAsync(createPaymentRequest);

            if (!createPaymentResponse.Status)
            {
                return ServiceResult.Error("Payment Failed", createPaymentResponse.ErrorMessage!, HttpStatusCode.BadRequest);
            }

            order.MarkAsPaid(createPaymentResponse.PaymentId!.Value);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);


            await publishEndpoint.Publish(new OrderCreatedEvent(order.OrderItems.Select(x => x.ProductId).ToList(), identityService.UserId));


            return ServiceResult.SuccessAsNoContent();


        }
    }
}
