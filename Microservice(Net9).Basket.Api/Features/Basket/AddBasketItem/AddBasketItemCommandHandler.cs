using Microservice_Net9_.Basket.Api.Data;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService, BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
            var newBasketItem = new BasketItem(
                CourseId: request.CourseId,
                CourseName: request.CourseName,
                CoursePrice: request.CoursePrice,
                ImageUrl: request.ImageUrl,
                UserId: request.UserId,
                EducatorFullName: request.EducatorFullName,
                PriceByApplyDiscount: null
            );


            var basketAsJson = await basketService.GetBasketJsonFromCacheAsync(cancellationToken);

            Data.Basket? currentBasket;

            if (string.IsNullOrEmpty(basketAsJson))
            {
                //currentBasket = new BasketDto(userId, [newBasketItem]);
                currentBasket = new Data.Basket(identityService.GetUserId, new List<BasketItem> { newBasketItem });

                await basketService.CreateBasketCashAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();

            }
            currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var existingBasketItems = currentBasket!.BasketItems.FirstOrDefault(x => x.CourseId == newBasketItem.CourseId);


            if (existingBasketItems is not null)
            {
                // TODO: business rule
                currentBasket.BasketItems.Remove(existingBasketItems);
            }

            currentBasket.BasketItems.Add(newBasketItem);
            currentBasket.ApplyExistingDiscount();  //if there is an existing discount, apply it to the new item


            await basketService.CreateBasketCashAsync(currentBasket, cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }


    }
}
