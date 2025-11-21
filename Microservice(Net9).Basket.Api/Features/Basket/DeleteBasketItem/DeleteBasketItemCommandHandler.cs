using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketJsonFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket could not found.", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            var hasCourse = currentBasket.BasketItems.FirstOrDefault(x => x.CourseId == request.CourseId);

            if (hasCourse is null)
            {
                return ServiceResult.Error("Basket item could not found.", $"Course {request.CourseId} is not in basket", HttpStatusCode.NotFound);
            }

            currentBasket.BasketItems.Remove(hasCourse);

            await basketService.CreateBasketCashAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
