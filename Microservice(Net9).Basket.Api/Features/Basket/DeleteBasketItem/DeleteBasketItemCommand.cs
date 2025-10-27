namespace Microservice_Net9_.Basket.Api.Features.Basket.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid CourseId) : IRequestByServiceResult;
}
