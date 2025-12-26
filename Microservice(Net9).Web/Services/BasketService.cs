using Microservice_Net9_.Basket.Api.Features.Basket.ApplyDiscountCoupon;
using Microservice_Net9_.Discount.Api.Features.Discounts;
using Microservice_Net9_.Web.Pages.Basket.Dtos;
using Microservice_Net9_.Web.Pages.Basket.ViewModels;
using Microservice_Net9_.Web.Pages.Order.ViewModels;
using Microservice_Net9_.Web.Services.Refit;
using System.Net;
using System.Threading.Tasks;

namespace Microservice_Net9_.Web.Services
{
    public class BasketService(
        IBasketRefitService basketRefitService,
        ILogger<BasketService> logger,
        UserService userService)
    {

        public async Task<ServiceResult> DeleteDiscountCouponAsync()
        {
            var response = await basketRefitService.ClearDiscountCouponAsync();
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult.Error("An error occurred while clearing discount from basket");
            }
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<(List<OrderItemViewModel> Items, float? DiscountRate)>> GetOrderItemViewModels()
        {
            var response = await basketRefitService.GetBasketAsync();
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return ServiceResult<(List<OrderItemViewModel>, float?)>.Success((new List<OrderItemViewModel>(), null));

                logger.LogError(response.Error.Message);
                return ServiceResult<(List<OrderItemViewModel>, float?)>.Error("An error occurred while getting the order items");
            }

            BasketDto basketDto = response.Content!;

            List<OrderItemViewModel> orderItemViewModelList = basketDto.BasketItems.Select(i => new OrderItemViewModel(i.CourseId, i.CourseName, i.CoursePrice)).ToList();


            return ServiceResult<(List<OrderItemViewModel>, float?)>.Success((orderItemViewModelList, basketDto.DiscountRate));
        }

        public async Task<ServiceResult> ApplyDiscountCouponAsync(DiscountDto discountDto)
        {

            if (!(userService.UserId == discountDto.UserId))
            {
                return ServiceResult.Error("The coupon code has not been assigned to this user");
            }
            if (discountDto.ExpireTime <= DateTime.UtcNow)
            {
                return ServiceResult.Error("The coupon code expired");
            }

            ApplyDiscountCouponRequest request = new(discountDto.Code, discountDto.Rate);

            var response = await basketRefitService.ApplyDiscountCouponAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult.Error("An error occurred while appling discount to basket");
            }
            return ServiceResult.Success();

        }

        public async Task<ServiceResult> DeleteBasketItemAsync(Guid courseId)
        {
            var response = await basketRefitService.DeleteBasketItemAsync(courseId);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult.Error("An error occurred while deleting item from basket");
            }
            return ServiceResult.Success();
        }

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

        //TODO: GetCourse Yapmam Lazım
        public async Task<ServiceResult> AddBasketItemAsync(AddBasketItemRequest request)
        {

            var response = await basketRefitService.AddBasketItemAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.Error.Message);
                return ServiceResult.Error("An error occurred while adding item to basket");
            }

            return ServiceResult.Success();
        }
    }
}
