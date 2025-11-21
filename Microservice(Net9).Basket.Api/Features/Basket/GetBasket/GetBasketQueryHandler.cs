using AutoMapper;
using Microservice_Net9_.Basket.Api.Dto;
using System.Text.Json;

namespace Microservice_Net9_.Basket.Api.Features.Basket.GetBasket
{
    public class GetBasketQueryHandler(IMapper mapper, BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {

            var basketAsJson = await basketService.GetBasketJsonFromCacheAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("Basket could not found", HttpStatusCode.NotFound);
            }

            Data.Basket currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson)!;

            BasketDto basketDto = mapper.Map<BasketDto>(currentBasket);

            return ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
