namespace Microservice_Net9_.Basket.Api.Dto
{
    //public record BasketDto
    //{
    //    public Guid UserId { get; init; }
    //    public List<BasketItemDto> BasketItems { get; init; } = new();
    //    public decimal TotalPrice { get; init; }

    //    // Custom constructor
    //    public BasketDto(Guid userId, List<BasketItemDto> basketItems)
    //    {
    //        UserId = userId;
    //        BasketItems = basketItems;
    //        TotalPrice = basketItems.Sum(item => item.CoursePrice);
    //    }
    //}

    public record BasketDto(Guid UserId, List<BasketItemDto> BasketItems);

}
