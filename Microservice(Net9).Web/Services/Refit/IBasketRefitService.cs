using Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon;
using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Services.ResponseDtos;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface IBasketRefitService 
    {
        [Get("/api/v1/baskets/user")]
        Task<ApiResponse<BasketDto>> GetBasketAsync();

        [Post("/api/v1/baskets/item")]
        Task<ApiResponse<object>> AddBasketItemAsync(AddBasketItemRequest addBasketItemRequest);

        [Delete("/api/v1/baskets/item/{courseId}")]
        Task<ApiResponse<object>> DeleteBasketItemAsync(Guid courseId);

        [Put("/api/v1/baskets/apply-discount-coupon")]
        Task<ApiResponse<object>> ApplyDiscountCouponAsync(ApplyDiscountCouponRequest applyDiscountCouponRequest);

        [Delete("/api/v1/baskets/clear-discount-coupon")]
        Task<ApiResponse<object>> ClearDiscountCouponAsync();
    }
}
