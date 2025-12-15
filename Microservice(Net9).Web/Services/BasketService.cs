using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using System.Net;
using System.Threading.Tasks;

namespace Microservice_Net9_.Web.Services
{
    public class BasketService(
        IBasketRefitService basketRefitService,
        ILogger<BasketService> logger)
    {
        public async Task<ServiceResult<BasketPageViewModel>> GetBasketAsync()
        {
            var response = await basketRefitService.GetBasketAsync();
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return ServiceResult<BasketPageViewModel>.Success(BasketPageViewModel.Empty());

                //TODO: logger.LogProblemDetails(responseAsResult.Error);
                logger.LogError(response.Error.Message);
                return ServiceResult<BasketPageViewModel>.Error("An error occurred while getting the baskets");
            }

            BasketDto basketDto = response.Content!;

            var basketViewModel =  new BasketPageViewModel()
            {
                Coupon = basketDto.CouponCode,
                DiscountRate = basketDto.DiscountRate,
                Items = basketDto.BasketItems.Select(x => new BasketViewModelItem(x.CourseId, x.ImageUrl, x.CourseName, x.CoursePrice, x.PriceByApplyDiscount)).ToList()
            };

            basketViewModel.SetPrice(basketDto.TotalPrice, basketDto.TotalPriceWithDiscount);


            return ServiceResult<BasketPageViewModel>.Success(basketViewModel);
        }
    }
}
