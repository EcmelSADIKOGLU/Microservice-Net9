using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Services.ResponseDtos;
using Refit;

namespace Microservice_Net9_.Web.Services.Refit
{
    public interface IBasketRefitService 
    {
        [Get("/api/v1/baskets/user")]
        Task<ApiResponse<BasketDto>> GetBasketAsync();
    }
}
