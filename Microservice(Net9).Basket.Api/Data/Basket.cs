using System.Text.Json.Serialization;

namespace Microservice_Net9_.Basket.Api.Data
{
    public class Basket
    {


        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? CouponCode { get; set; }

        [JsonIgnore]
        public bool IsApplyDiscount => DiscountRate.HasValue && DiscountRate > 0 && !string.IsNullOrEmpty(CouponCode);

        [JsonIgnore]
        public decimal TotalPrice => BasketItems.Sum(item => item.CoursePrice);

        [JsonIgnore]
        public decimal TotalPriceWithDiscount => IsApplyDiscount ? 
            BasketItems.Sum(item => item.PriceByApplyDiscount ?? item.CoursePrice) : 
            TotalPrice;

        public Basket()
        {
                
        }

        public Basket(Guid userId, List<BasketItem> basketItems)
        {
            UserId = userId;
            BasketItems = basketItems;
        }

        public void ApplyNewDiscount(string couponCode, float discountRate)
        {
            CouponCode = couponCode;
            DiscountRate = discountRate;

            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscount = item.CoursePrice * (decimal)(1 - discountRate);
            }
        }

        public void ApplyExistingDiscount()
        {
            if (IsApplyDiscount)
            {
                foreach (var item in BasketItems)
                {
                    item.PriceByApplyDiscount = item.CoursePrice * (decimal)(1 - DiscountRate!.Value);
                }
            }

        }

        public void ClearDiscount()
        {
            CouponCode = null;
            DiscountRate = null;
            foreach (var item in BasketItems)
            {
                item.PriceByApplyDiscount = null;
            }
        }


    }
}
