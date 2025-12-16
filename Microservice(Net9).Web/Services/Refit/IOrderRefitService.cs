using Microservice_Net9_.Discount.Api.Features.Discounts;
using Microservice_Net9_.Web.Pages.Order.Dtos;
using Microservice_Net9_.Web.Services.ResponseDtos.Order;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface IOrderRefitService
    {
        [Post("/api/v1/orders")]
        Task<ApiResponse<object>> CreateOrderAsync(CreateOrderRequest createOrderRequest);

        [Get("/api/v1/orders/user")]
        Task<ApiResponse<GetOrdersByBuyerIdResponse>> GetOrdersByBuyerIdAsync();
    }
}
