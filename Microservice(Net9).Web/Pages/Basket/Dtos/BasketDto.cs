using System.Text.Json.Serialization;

namespace Microservice_Net9_.Web.Pages.Basket.Dtos
{
    public record BasketDto ()
    {

        public bool IsApplyDiscount => DiscountRate.HasValue && DiscountRate > 0 && !string.IsNullOrEmpty(CouponCode);

        public List<BasketItemDto> BasketItems { get; init; } = new();
        public float? DiscountRate { get; set; }
        public string? CouponCode { get; set; }

        
        public decimal TotalPrice => BasketItems.Sum(item => item.CoursePrice);

        public decimal TotalPriceWithDiscount => IsApplyDiscount ?
            BasketItems.Sum(item => item.PriceByApplyDiscount ?? item.CoursePrice) :
            TotalPrice;
    }


}
