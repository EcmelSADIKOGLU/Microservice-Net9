using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Basket.Api.Dto;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {

            Guid userId = identityService.GetUserId;


            var newBasketItem = new BasketItemDto(
                CourseId: request.CourseId,
                CourseName: request.CourseName,
                CoursePrice: request.CoursePrice,
                ImageUrl: request.ImageUrl,
                UserId: userId,
                EducatorFullName: request.EducatorFullName,
                PriceByApplyDiscount: null
            );


            var casheKey = string.Format(BasketConst.BasketCacheKey, userId); //0 yazan yere userId gelecek

            var basketAsJson = await distributedCache.GetStringAsync(casheKey, cancellationToken);

            BasketDto? currentBasket;

            if (string.IsNullOrEmpty(basketAsJson))
            {
                //currentBasket = new BasketDto(userId, [newBasketItem]);
                currentBasket = new BasketDto(userId, new List<BasketItemDto> { newBasketItem });

                await CreateCash(currentBasket, casheKey, cancellationToken);
                return ServiceResult.SuccessAsNoContent();

            }
            currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsJson);

            var existingBasketItems = currentBasket!.BasketItems.FirstOrDefault(x => x.CourseId == newBasketItem.CourseId);


            if (existingBasketItems is not null)
            {
                // TODO: business rule
                currentBasket.BasketItems.Remove(existingBasketItems);
            }

            currentBasket.BasketItems.Add(newBasketItem);

            await CreateCash(currentBasket, casheKey, cancellationToken);
            return ServiceResult.SuccessAsNoContent();

        }

        private async Task CreateCash(BasketDto basketDto, string casheKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basketDto);
            await distributedCache.SetStringAsync(casheKey, basketAsString, cancellationToken);
        }
    }
}
