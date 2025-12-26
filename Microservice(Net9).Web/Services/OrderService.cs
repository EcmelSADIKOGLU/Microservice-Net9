using Microservice_Net9_.Discount.Api.Features.Discounts;
using Microservice_Net9_.Web.Pages.Order.Dtos;
using Microservice_Net9_.Web.Pages.Order.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Microservice_Net9_.Web.Services
{
    public class OrderService(
        IOrderRefitService orderRefitService, 
        ILogger<OrderService> logger)
    {
        public async Task<ServiceResult> CreateOrderAsync(CreateOrderViewModel model)
        {

            CreateOrderRequest createOrderRequest = new(
                model.DiscountRate,
                new AddressDto(
                    model.Address.Province,
                    model.Address.District,
                    model.Address.Street,
                    model.Address.ZipCode,
                    model.Address.Line),
                new PaymentDto(
                    model.Payment.CardNumber,
                    model.Payment.CardHolderName,
                    DateTime.ParseExact( model.Payment.ExpiryDate, "MM/yy", CultureInfo.InvariantCulture).AddMonths(1).AddSeconds(-1),
                    model.Payment.Cvv),
                model.OrderItems.Select(i => new OrderItemDto(i.ProductId, i.ProductName, i.UnitPrice)).ToList());

            var response = await orderRefitService.CreateOrderAsync(createOrderRequest);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult.Error("An error occurred while creating order");
            }

            return ServiceResult.Success();
        }
    }
}
