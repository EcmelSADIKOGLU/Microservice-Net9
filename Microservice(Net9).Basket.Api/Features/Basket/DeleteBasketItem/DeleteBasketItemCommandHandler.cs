using Microservice_Net9_.Basket.Api.Const;
using Microservice_Net9_.Basket.Api.Dto;
using Microservice_Net9_.Shared.Services;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache, IIdentityService identityService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {

            Guid userId = identityService.GetUserId;

            var casheKey = string.Format(BasketConst.BasketCacheKey, userId); //0 yazan yere userId gelecek
            var basketAsJson = await distributedCache.GetStringAsync(casheKey, cancellationToken);


            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found.", $"User {userId} does not have a basket", HttpStatusCode.NotFound);
            }

            BasketDto currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsJson)!;

            var hasCourse = currentBasket.BasketItems.FirstOrDefault(x => x.CourseId == request.CourseId);

            if (hasCourse is null)
            {
                return ServiceResult.Error("Basket item could not found.", $"Course {request.CourseId} is not in basket", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItems.Remove(hasCourse);

            var basketAsString = JsonSerializer.Serialize(currentBasket);
            await distributedCache.SetStringAsync(casheKey, basketAsString, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
