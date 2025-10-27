using AutoMapper;
using Microservice_Net9_.Basket.Api.Dto;
using Microservice_Net9_.Basket.Api.Features.Basket.AddBasketItem;

namespace Microservice_Net9_.Basket.Api.Features.Basket
{
    public class BasketMapper : Profile
    {
        public BasketMapper()
        {
            CreateMap<AddBasketItemCommand, BasketItemDto>();
        }
    }
}
